package com.CoRangE.BookStar.util;

import java.math.BigInteger;
import java.time.LocalDateTime;
import java.time.ZoneOffset;

public class DateTimeToBigIntegerConverter {
    public static BigInteger dateTime() {
        LocalDateTime now = LocalDateTime.now(ZoneOffset.UTC);
        return BigInteger.valueOf(now.toEpochSecond(ZoneOffset.UTC));
    }
}
