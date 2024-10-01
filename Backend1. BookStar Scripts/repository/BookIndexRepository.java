package com.CoRangE.BookStar.repository;

import com.CoRangE.BookStar.entity.BookIndex;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.UUID;

@Repository
public interface BookIndexRepository extends JpaRepository<BookIndex, UUID> {
}
