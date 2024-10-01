package com.CoRangE.BookStar.controller;

import com.CoRangE.BookStar.dto.JwtAuthenticationResponse;
import com.CoRangE.BookStar.dto.RefreshTokenRequest;
import com.CoRangE.BookStar.dto.SignInRequest;
import com.CoRangE.BookStar.dto.SignUpRequest;
import com.CoRangE.BookStar.entity.User;
import com.CoRangE.BookStar.service.AuthenticationService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@RestController
@RequestMapping("/auth")
@RequiredArgsConstructor
public class AuthenticationController {

    private final AuthenticationService authenticationService;

    @PostMapping("/signup")
    public ResponseEntity<User> signup(@RequestParam("email") String email, @RequestParam("password") String password){
        SignUpRequest signUpRequest = new SignUpRequest();
        signUpRequest.setEmail(email);
        signUpRequest.setPassword(password);
        return ResponseEntity.ok(authenticationService.signup(signUpRequest));
    }

    @PostMapping("/signIn")
    public ResponseEntity<JwtAuthenticationResponse> signIn(@RequestParam("email") String email, @RequestParam("password") String password){
        SignInRequest signInRequest = new SignInRequest();
        signInRequest.setEmail(email);
        signInRequest.setPassword(password);
        return ResponseEntity.ok(authenticationService.signIn(signInRequest));
    }

    @PostMapping("/refresh")
    public ResponseEntity<JwtAuthenticationResponse> refresh(@RequestParam("token") String token){
        RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest();
        refreshTokenRequest.setToken(token);
        return ResponseEntity.ok(authenticationService.refreshToken(refreshTokenRequest));
    }
}
