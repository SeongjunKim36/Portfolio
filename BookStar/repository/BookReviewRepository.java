package com.CoRangE.BookStar.repository;

import com.CoRangE.BookStar.entity.BookReview;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.UUID;

@Repository
public interface BookReviewRepository extends JpaRepository<BookReview, UUID> {
}
