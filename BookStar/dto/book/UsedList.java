package com.CoRangE.BookStar.dto.book;

public class UsedList {
    private AladinUsed aladinUsed;
    private UserUsed userUsed;

    public AladinUsed getAladinUsed() {
        return aladinUsed;
    }

    public void setAladinUsed(AladinUsed aladinUsed) {
        this.aladinUsed = aladinUsed;
    }

    public UserUsed getUserUsed() {
        return userUsed;
    }

    public void setUserUsed(UserUsed userUsed) {
        this.userUsed = userUsed;
    }
}
