package com.CoRangE.BookStar.service.impl;

import com.CoRangE.BookStar.dto.book.BookInfo;
import com.CoRangE.BookStar.dto.book.BookSearch;
import com.CoRangE.BookStar.entity.Author;
import com.CoRangE.BookStar.entity.Book;
import com.CoRangE.BookStar.repository.APIBookRepository;
import com.CoRangE.BookStar.repository.DBBookRepository;
import com.CoRangE.BookStar.service.BookService;
import org.springframework.stereotype.Service;

import java.math.BigInteger;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

@Service
public class BookServiceImpl implements BookService {
    private final APIBookRepository apiBookRepository;
    private final DBBookRepository dbBookRepository;

    public BookServiceImpl(APIBookRepository apiBookRepository, DBBookRepository dbBookRepository) {
        this.apiBookRepository = apiBookRepository;
        this.dbBookRepository = dbBookRepository;
    }

    @Override
    public List<Book> searchBook(String keyword) {
        List<BookInfo> bookInfos = apiBookRepository.searchBook(keyword);
        List<Book> books = new ArrayList<>();
        for (BookInfo bookInfo : bookInfos) {
            Book book = new Book();
            book.setId(UUID.randomUUID());
            book.setTitle(bookInfo.getTitle());
            book.setIsbn(bookInfo.getIsbn());
            Author author = new Author();
            author.setId(UUID.randomUUID());
            author.setName(bookInfo.getAuthor());

            book.setCreatedAt(new Date());
            book.setUpdatedAt(new Date());

            book.setUser(null);
            book.setDescription("");
            book.setDeletedAt(null);
            if (!dbBookRepository.existsByIsbn(book.getIsbn())) {
                dbBookRepository.save(book);
            }
            books.add(book);
        }
        return books;
    }

}
