package com.CoRangE.BookStar.dto.book;

import java.util.List;

public class Ebook {
    private String subTitle;
    private String originalTitle;
    private int itemPage;
    private List<String> previewImgList;

    private List<Ebook> ebookList;

    public String getSubTitle() {
        return subTitle;
    }

    public void setSubTitle(String subTitle) {
        this.subTitle = subTitle;
    }

    public String getOriginalTitle() {
        return originalTitle;
    }

    public void setOriginalTitle(String originalTitle) {
        this.originalTitle = originalTitle;
    }

    public int getItemPage() {
        return itemPage;
    }

    public void setItemPage(int itemPage) {
        this.itemPage = itemPage;
    }

    public List<String> getPreviewImgList() {
        return previewImgList;
    }

    public void setPreviewImgList(List<String> previewImgList) {
        this.previewImgList = previewImgList;
    }

    public List<Ebook> getEbookList() {
        return ebookList;
    }

    public void setEbookList(List<Ebook> ebookList) {
        this.ebookList = ebookList;
    }


}
