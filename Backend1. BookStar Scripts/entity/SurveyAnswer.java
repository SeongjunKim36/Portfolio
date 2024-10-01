package com.CoRangE.BookStar.entity;

import jakarta.persistence.*;
import lombok.Data;
import lombok.Getter;
import lombok.Setter;

import java.util.Date;
import java.util.List;
import java.util.UUID;

@Setter
@Getter
@Data
@Entity
@Table(name = "surveyAnswer")
public class SurveyAnswer {
    @Id
    private UUID id;

    @ManyToOne
    @JoinColumn(name = "surveyId", referencedColumnName = "id", nullable = false)
    private Survey survey;

    @Column(name = "content", nullable = false)
    private String content;

    @Column(name = "createdAt", nullable = false)
    private Date createdAt;

    @Column(name = "updatedAt", nullable = false)
    private Date updatedAt;

    @Column(name = "deletedAt")
    private Date deletedAt;

}
