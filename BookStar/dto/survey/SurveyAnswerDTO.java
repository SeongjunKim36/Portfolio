package com.CoRangE.BookStar.dto.survey;
import lombok.Data;
import lombok.Getter;
import lombok.Setter;

import java.util.UUID;

@Setter
@Getter
@Data
public class SurveyAnswerDTO {
    private UUID id;
    private UUID surveyId;
    private String content;
}