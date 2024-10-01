package com.CoRangE.BookStar.service.impl;

import com.CoRangE.BookStar.dto.survey.SurveyDTO;
import com.CoRangE.BookStar.dto.survey.SurveyRequest;
import com.CoRangE.BookStar.entity.*;
import com.CoRangE.BookStar.repository.*;
import com.CoRangE.BookStar.service.UserService;
import jakarta.transaction.Transactional;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
@RequiredArgsConstructor
public class UserServiceImpl implements UserService {

    @Autowired
    private final UserRepository userRepository;
    @Autowired
    private final SurveyRepository surveyRepository;
    @Autowired
    private final UserSurveyAnswerRepository userSurveyAnswerRepository;

    @Override
    public User getUserByEmail(String email) {
        return userRepository.findByEmail(email)
                .orElseThrow(() -> new UsernameNotFoundException("User not found"));
    }
    @Override
    public UserDetailsService userDetailsService() {
        return new UserDetailsService() {
            @Override
            public UserDetails loadUserByUsername(String username) {
                return userRepository.findByEmail(username)
                        .orElseThrow(() -> new UsernameNotFoundException("User not found"));
            }
        };
    }
    @Override
    public List<SurveyDTO> getSurvey() {
        // 모든 설문조사를 가져옴
        List<Survey> surveys = surveyRepository.findAll();

        // Survey를 SurveyDTO로 변환
        List<SurveyDTO> surveyDTOs = new ArrayList<>();
        for (Survey survey : surveys) {
            SurveyDTO surveyDTO = new SurveyDTO();
            surveyDTO.setId(survey.getId());
            surveyDTO.setSurveyNumber(survey.getSurveyNumber());
            surveyDTO.setContent(survey.getContent());
            surveyDTO.setCheckCount(survey.getCheckCount());

            surveyDTOs.add(surveyDTO);
        }

        return surveyDTOs;
    }
    @Override
    @Transactional
    public void answerSurvey(Integer order, List<Integer> contentValues) {
        // 사용자 인증 정보를 가져옵니다.
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        String currentPrincipalName = authentication.getName();

        Optional<User> userOptional = userRepository.findByEmail(currentPrincipalName);

        if (userOptional.isPresent()) {
            User user = userOptional.get();

            List<Survey> surveys = surveyRepository.findAll();

            if (order < surveys.size()) {
                Survey survey = surveys.get(order);
                List<SurveyAnswer> surveyAnswers = survey.getSurveyAnswers();

                for (Integer contentValue : contentValues) {
                    if (contentValue - 1 < surveyAnswers.size()) {
                        SurveyAnswer surveyAnswer = surveyAnswers.get(contentValue - 1);

                        UserSurveyAnswer userSurveyAnswer = new UserSurveyAnswer();
                        userSurveyAnswer.setId(UUID.randomUUID());
                        userSurveyAnswer.setUser(user);
                        userSurveyAnswer.setSurvey(survey);
                        userSurveyAnswer.setSurveyAnswer(surveyAnswer);
                        userSurveyAnswer.setCreatedAt(new Date());
                        userSurveyAnswer.setUpdatedAt(new Date());

                        userSurveyAnswerRepository.save(userSurveyAnswer);
                    }
                }
            }
        }
    }
}
