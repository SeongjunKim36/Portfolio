package com.CoRangE.BookStar.entity;

import jakarta.persistence.*;
import lombok.Data;

import java.math.BigInteger;
import java.util.Date;
import java.util.UUID;

@Data
@Entity
@Table(name = "user_book")
public class UserBook {
    @Id
    private UUID id;

    @ManyToOne
    @JoinColumn(name = "user_id", referencedColumnName = "id")
    private User user;

    @ManyToOne
    @JoinColumn(name = "book_id", referencedColumnName = "id")
    private Book book;

    @Column(name = "progress_pctg", nullable = false, columnDefinition = "int default 0")
    private int progressPercentage;

    @Column(name = "progress_page", nullable = false, columnDefinition = "int default 0")
    private int progressPage;

    @Column(name = "rating", columnDefinition = "int default 0")
    private int rating;

    @Column(name = "status", nullable = false)
    private int status;

    @Column(name = "memo")
    private String memo;

    @Column(name = "readStartedAt", nullable = false)
    private BigInteger readStartedAt;

    @Column(name = "readEndedAt", nullable = false)
    private BigInteger readEndedAt;

    @Column(name = "createdAt", nullable = false)
    private Date createdAt;

    @Column(name = "updatedAt", nullable = false)
    private Date updatedAt;

    @Column(name = "deletedAt", nullable = false)
    private Date deletedAt;
}
