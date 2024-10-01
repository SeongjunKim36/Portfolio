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
@Table(name = "survey")
public class Survey {
    @Id
    private UUID id;

    @Column(name = "surveyNum", nullable = false)
    private int surveyNumber;

    @Column(name = "content", nullable = false, length = 255)
    private String content;

    @Column(name = "checkCount", nullable = false)
    private short checkCount;

    @Column(name = "createdAt", nullable = false)
    private Date createdAt;

    @Column(name = "updatedAt", nullable = false)
    private Date updatedAt;

    @Column(name = "deletedAt")
    private Date deletedAt;

    @OneToMany(mappedBy = "survey")
    private List<SurveyAnswer> surveyAnswers;

    @Override
    public String toString() {
        return "Survey{" +
                "id=" + id +
                ", surveyNumber=" + surveyNumber +
                ", content='" + content + '\'' +
                ", checkCount=" + checkCount +
                ", createdAt=" + createdAt +
                ", updatedAt=" + updatedAt +
                ", deletedAt=" + deletedAt +
                '}';
    }

}
