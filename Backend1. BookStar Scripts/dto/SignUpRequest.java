package com.CoRangE.BookStar.dto;

import lombok.Data;
import lombok.Getter;
import lombok.Setter;

import java.util.UUID;

@Setter
@Getter
@Data
public class SignUpRequest {

    private UUID id;
    private String email;
    private String password;

}
