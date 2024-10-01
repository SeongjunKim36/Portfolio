package com.CoRangE.BookStar.repository;

import com.CoRangE.BookStar.entity.SurveyAnswer;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;
import java.util.UUID;

@Repository
public interface SurveyAnswerRepository extends JpaRepository<SurveyAnswer, UUID> {
    Optional<SurveyAnswer> findByContent(String contentValue);
}
