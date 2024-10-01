package com.CoRangE.BookStar;

import com.CoRangE.BookStar.entity.Role;
import com.CoRangE.BookStar.entity.Survey;
import com.CoRangE.BookStar.entity.SurveyAnswer;
import com.CoRangE.BookStar.entity.User;
import com.CoRangE.BookStar.repository.SurveyAnswerRepository;
import com.CoRangE.BookStar.repository.SurveyRepository;
import com.CoRangE.BookStar.repository.UserRepository;
import com.CoRangE.BookStar.service.impl.UserServiceImpl;
import com.CoRangE.BookStar.util.LambdaApiGatewayFunction;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;

import java.math.BigInteger;
import java.util.*;
import java.util.function.Function;

@SpringBootApplication
public class BookStarApplication implements CommandLineRunner {

	@Autowired
	private UserRepository userRepository;
	@Autowired
	private SurveyRepository surveyRepository;

	@Autowired
	private SurveyAnswerRepository surveyAnswerRepository;
	@Bean
	public Function<Map<String, Object>, List<String>> lambdaApiGatewayFunctionBean() {
		return new LambdaApiGatewayFunction();
	}

	public static void main(String[] args) {
		SpringApplication.run(BookStarApplication.class, args);
	}

	public void run(String... args){
		// Survey 객체를 먼저 생성하고 저장합니다.
		Survey survey = new Survey();
		survey.setId(UUID.randomUUID());
		survey.setSurveyNumber(1);
		survey.setContent("Test survey content");
		survey.setCheckCount((short) 1);
		survey.setCreatedAt(new Date());
		survey.setUpdatedAt(new Date());
		survey = surveyRepository.save(survey);

		List<SurveyAnswer> surveyAnswers = new ArrayList<>();

		SurveyAnswer surveyAnswer1 = new SurveyAnswer();
		surveyAnswer1.setId(UUID.randomUUID());
		surveyAnswer1.setContent("남자");
		surveyAnswer1.setCreatedAt(new Date());
		surveyAnswer1.setUpdatedAt(new Date());
		surveyAnswer1.setSurvey(survey);

		surveyAnswers.add(surveyAnswer1);

		SurveyAnswer surveyAnswer2 = new SurveyAnswer();
		surveyAnswer2.setId(UUID.randomUUID());
		surveyAnswer2.setContent("여자");
		surveyAnswer2.setCreatedAt(new Date());
		surveyAnswer2.setUpdatedAt(new Date());
		surveyAnswer2.setSurvey(survey);

		surveyAnswers.add(surveyAnswer2);

		surveyAnswerRepository.saveAll(surveyAnswers);


		survey.setSurveyAnswers(surveyAnswers);


		surveyRepository.save(survey);


//		User adminAccount = userRepository.findByRole(Role.ADMIN);
//		if (null == adminAccount){
//			User user = new User();
//
//			user.setEmail("admin");
//			user.setRole(Role.ADMIN);
//			user.setPassword(new BCryptPasswordEncoder().encode("admin"));
//			user.setNickname("admin");
//			user.setSignType(0); // Assuming 0 is for internal users
//			user.setCreatedAt(new Date());
//			user.setUpdatedAt(new Date());
//			user.setDeletedAt(null); // Assuming the user is not deleted
//
//			userRepository.save(user);
//		}
	}
}
