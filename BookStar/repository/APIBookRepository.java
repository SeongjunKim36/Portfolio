package com.CoRangE.BookStar.repository;


import com.CoRangE.BookStar.dto.book.BookInfo;
import com.CoRangE.BookStar.dto.book.BookSearch;
import com.CoRangE.BookStar.entity.Book;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.http.converter.HttpMessageConverter;
import org.springframework.stereotype.Repository;
import org.springframework.web.client.HttpClientErrorException;
import org.springframework.web.client.HttpServerErrorException;
import org.springframework.web.client.ResourceAccessException;
import org.springframework.web.client.RestTemplate;
import org.springframework.web.util.UriComponentsBuilder;

import java.util.ArrayList;
import java.util.List;

@Repository
public class APIBookRepository implements BookRepository{
    @Autowired
    private RestTemplate restTemplate;

    private final DBBookRepository dbBookRepository;

    public APIBookRepository(RestTemplate restTemplate, DBBookRepository dbBookRepository) {
        this.restTemplate = restTemplate;
        this.dbBookRepository = dbBookRepository;
    }
    public List<BookInfo> searchBook(String keyword) {
        String url = "http://www.aladin.co.kr/ttb/api/ItemSearch.aspx";
        String ttbKey = "ttbdong23071445001";
        UriComponentsBuilder builder = UriComponentsBuilder.fromHttpUrl(url)
                .queryParam("TTBKey", ttbKey)
                .queryParam("Query", keyword)
                .queryParam("QueryType", "Keyword")
                .queryParam("MaxResults", 10)
                .queryParam("start", 1)
                .queryParam("SearchTarget", "Book")
                .queryParam("output", "js")
                .queryParam("Version", "20131101");

//        String uri = builder.toUriString();
//        RestTemplate restTemplate = new RestTemplate();
//
//        ResponseEntity<BookSearch> response = restTemplate.getForEntity(uri, BookSearch.class);
//        BookSearch bookSearch = response.getBody();
//
//
//        List<BookInfo> bookInfoList = bookSearch.getItem();
//        List<Book> books = new ArrayList<>();
//
//        for (BookInfo bookInfo : bookInfoList) {
//            System.out.println("Title: " + bookInfo.getTitle());
//            if (dbBookRepository.existsByIsbn(bookInfo.getIsbn())) {
//                continue;
//            } else {
//                dbBookRepository.save(bookInfo);
//            }
//            Book book = dbBookRepository.findByIsbn(bookInfo.getIsbn());
//            books.add(book);
//        }
//
//        return books;

        String uri = builder.toUriString();
        RestTemplate restTemplate = new RestTemplate();

        ResponseEntity<BookSearch> response = restTemplate.getForEntity(uri, BookSearch.class);
        BookSearch bookSearch = response.getBody();
        List<BookInfo> bookInfos = bookSearch.getItem();
        List<BookInfo> result = new ArrayList<>();

        for (BookInfo bookInfo : bookInfos) {
            System.out.println("Title: " + bookInfo.getTitle());
            result.add(bookInfo);
        }

        return result;
    }
}
