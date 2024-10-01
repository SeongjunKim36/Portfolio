package com.CoRangE.BookStar.dto.survey;

import lombok.Data;
import lombok.Getter;
import lombok.Setter;

import java.util.List;
import java.util.UUID;

@Setter
@Getter
@Data
public class SurveyRequest {
    private UUID userId;
    private List<UUID> answerIds;
}
