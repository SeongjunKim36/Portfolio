package com.CoRangE.BookStar.controller;

import com.CoRangE.BookStar.entity.Book;
import com.CoRangE.BookStar.service.BookService;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/book")
public class BookController {
    private final BookService bookService;
    @GetMapping("/search")
    public List<Book> searchBook(@RequestParam(name = "keyword") String keyword) {
        return bookService.searchBook(keyword);
    }
}
