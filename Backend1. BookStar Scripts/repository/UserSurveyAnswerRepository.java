package com.CoRangE.BookStar.repository;

import com.CoRangE.BookStar.entity.UserSurveyAnswer;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface UserSurveyAnswerRepository extends JpaRepository<UserSurveyAnswer, UUID> {
}
