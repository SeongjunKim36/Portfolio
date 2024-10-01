package com.CoRangE.BookStar.controller;

import com.CoRangE.BookStar.dto.SignUpRequest;
import com.CoRangE.BookStar.dto.survey.SurveyDTO;
import com.CoRangE.BookStar.dto.survey.SurveyRequest;
import com.CoRangE.BookStar.entity.Survey;
import com.CoRangE.BookStar.entity.User;
import com.CoRangE.BookStar.entity.UserSurveyAnswer;
import com.CoRangE.BookStar.service.UserService;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/user")
@RequiredArgsConstructor
public class UserController {
    @Autowired
    private final UserService userService;
    @GetMapping("/info")
    public ResponseEntity<User> getUserByEmail(@RequestParam("email") String email) {
        User user = userService.getUserByEmail(email);
        return ResponseEntity.ok(user);
    }
    @GetMapping
    public ResponseEntity<String> checkAccess(){
        return ResponseEntity.ok("USER");
    }
    @GetMapping("/Survey")
    public ResponseEntity<List<SurveyDTO>> getSurveys(){
        return ResponseEntity.ok(userService.getSurvey());
    }
    @PostMapping("/AnswerSurvey")
    public ResponseEntity<Void> answerSurvey(@RequestParam("contentValues") List<List<Integer>> contentValuesList){
        for (int i = 0; i < contentValuesList.size(); i++) {
            userService.answerSurvey(i, contentValuesList.get(i));
        }
        return ResponseEntity.ok().build();
    }
}
