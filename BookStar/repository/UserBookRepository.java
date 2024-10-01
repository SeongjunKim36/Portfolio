package com.CoRangE.BookStar.repository;

import com.CoRangE.BookStar.entity.UserBook;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.UUID;

@Repository
public interface UserBookRepository extends JpaRepository<UserBook, UUID> {
}
