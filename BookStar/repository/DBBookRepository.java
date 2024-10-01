package com.CoRangE.BookStar.repository;

import com.CoRangE.BookStar.dto.book.BookInfo;
import com.CoRangE.BookStar.entity.Author;
import com.CoRangE.BookStar.entity.Book;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface DBBookRepository extends JpaRepository<Book, Long>{

    boolean existsByIsbn(String isbn);
    Book findByIsbn(String isbn);
}
