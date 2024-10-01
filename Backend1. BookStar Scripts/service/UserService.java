package com.CoRangE.BookStar.service;

import com.CoRangE.BookStar.dto.survey.SurveyDTO;
import com.CoRangE.BookStar.dto.survey.SurveyRequest;
import com.CoRangE.BookStar.entity.Survey;
import com.CoRangE.BookStar.entity.User;
import com.CoRangE.BookStar.entity.UserSurveyAnswer;
import org.springframework.security.core.userdetails.UserDetailsService;

import java.util.List;

public interface UserService {
    UserDetailsService userDetailsService();
    void answerSurvey(Integer order, List<Integer> contentValues);
    List<SurveyDTO> getSurvey();
    User getUserByEmail(String email);
}
