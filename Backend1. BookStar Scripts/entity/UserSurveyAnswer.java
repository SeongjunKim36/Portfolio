package com.CoRangE.BookStar.entity;

import jakarta.persistence.*;
import lombok.Data;
import lombok.Getter;
import lombok.Setter;

import java.util.Date;
import java.util.UUID;

@Setter
@Getter
@Data
@Entity
@Table(name = "userSurveyAnswer")
public class UserSurveyAnswer {
    @Id
    private UUID id;

    @ManyToOne
    @JoinColumn(name = "userId", referencedColumnName = "id", nullable = false)
    private User user;

    @ManyToOne
    @JoinColumn(name = "surveyId", referencedColumnName = "id", nullable = false)
    private Survey survey;

    @ManyToOne
    @JoinColumn(name = "answer", referencedColumnName = "id", nullable = false)
    private SurveyAnswer surveyAnswer;

    @Column(name = "createdAt", nullable = false)
    private Date createdAt;

    @Column(name = "updatedAt", nullable = false)
    private Date updatedAt;

    @Column(name = "deletedAt")
    private Date deletedAt;

}
