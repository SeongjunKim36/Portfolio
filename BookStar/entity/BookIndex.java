package com.CoRangE.BookStar.entity;

import jakarta.persistence.*;
import lombok.Data;

import java.math.BigInteger;
import java.util.Date;
import java.util.UUID;

@Data
@Entity
@Table(name = "book_index")
public class BookIndex {
    @Id
    private UUID id;

    @ManyToOne
    @JoinColumn(name = "book_id", referencedColumnName = "id")
    private Book book;

    @Column(name = "title", nullable = false)
    private String title;

    @Column(name = "page", nullable = false)
    private int page;

    @Column(name = "createdAt", nullable = false)
    private Date createdAt;

    @Column(name = "updatedAt")
    private Date updatedAt;

    @Column(name = "deletedAt")
    private Date deletedAt;
}
