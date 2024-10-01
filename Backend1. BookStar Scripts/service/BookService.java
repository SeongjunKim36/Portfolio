package com.CoRangE.BookStar.service;

import com.CoRangE.BookStar.dto.book.BookInfo;
import com.CoRangE.BookStar.dto.book.BookSearch;
import com.CoRangE.BookStar.entity.Book;

import java.util.List;

public interface BookService {
    List<Book> searchBook(String keyword);

}
