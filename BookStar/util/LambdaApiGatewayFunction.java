package com.CoRangE.BookStar.util;

import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.function.Function;

@Component
@Slf4j
public class LambdaApiGatewayFunction implements Function<Map<String, Object>, List<String>> {
    @Override
    public List<String> apply(Map<String, Object> map) {
        log.info("lambda apply 수행");

        List<String> strings = new ArrayList<>();
        strings.add(map.get("title").toString());
        strings.add(map.get("name").toString());
        strings.add("sample");

        log.info("lambda apply 종료");

        return strings;
    }
}
