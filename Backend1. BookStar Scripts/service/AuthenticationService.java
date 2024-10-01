package com.CoRangE.BookStar.service;

import com.CoRangE.BookStar.dto.JwtAuthenticationResponse;
import com.CoRangE.BookStar.dto.RefreshTokenRequest;
import com.CoRangE.BookStar.dto.SignInRequest;
import com.CoRangE.BookStar.dto.SignUpRequest;
import com.CoRangE.BookStar.entity.User;

public interface AuthenticationService {

    User signup(SignUpRequest signUpRequest);

    JwtAuthenticationResponse signIn(SignInRequest signInRequest);

    JwtAuthenticationResponse refreshToken(RefreshTokenRequest refreshTokenRequest);
}
