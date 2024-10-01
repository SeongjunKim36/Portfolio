package com.CoRangE.BookStar.dto.survey;

import lombok.Data;
import lombok.Getter;
import lombok.Setter;
import java.util.UUID;

@Setter
@Getter
@Data
public class SurveyDTO {
    private UUID id;
    private int surveyNumber;
    private String content;
    private short checkCount;
}