package com.CoRangE.BookStar.controller;

import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/healthCheck")
@RequiredArgsConstructor
public class HealthCheckController {

    @GetMapping
    public ResponseEntity<String> checkAccess() {
        return ResponseEntity.ok("success");
    }
}
