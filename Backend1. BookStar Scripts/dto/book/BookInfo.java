package com.CoRangE.BookStar.dto.book;

import java.util.List;

public class BookInfo {
    private String title;
    private String subTitle;
    private String originalTitle;
    private int itemPage;
    private List<String> previewImgList;
    private List<Ebook> ebookList;
    private String link;
    private String author;
    private String pubDate;
    private String description;
    private String isbn;
    private String isbn13;
    private int itemId;
    private int priceSales;
    private int priceStandard;
    private String mallType;
    private String stockStatus;
    private int mileage;
    private String cover;
    private String publisher;
    private int salesPoint;
    private boolean fixedPrice;
    private int customerReviewRank;
    private SeriesInfo seriesInfo;
    private SubInfo subInfo;

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }
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


    public String getLink() {
        return link;
    }

    public void setLink(String link) {
        this.link = link;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public String getPubDate() {
        return pubDate;
    }

    public void setPubDate(String pubDate) {
        this.pubDate = pubDate;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getIsbn() {
        return isbn;
    }

    public void setIsbn(String isbn) {
        this.isbn = isbn;
    }

    public String getIsbn13() {
        return isbn13;
    }

    public void setIsbn13(String isbn13) {
        this.isbn13 = isbn13;
    }

    public int getItemId() {
        return itemId;
    }

    public void setItemId(int itemId) {
        this.itemId = itemId;
    }

    public int getPriceSales() {
        return priceSales;
    }

    public void setPriceSales(int priceSales) {
        this.priceSales = priceSales;
    }

    public int getPriceStandard() {
        return priceStandard;
    }

    public void setPriceStandard(int priceStandard) {
        this.priceStandard = priceStandard;
    }

    public String getMallType() {
        return mallType;
    }

    public void setMallType(String mallType) {
        this.mallType = mallType;
    }

    public String getStockStatus() {
        return stockStatus;
    }

    public void setStockStatus(String stockStatus) {
        this.stockStatus = stockStatus;
    }

    public int getMileage() {
        return mileage;
    }

    public void setMileage(int mileage) {
        this.mileage = mileage;
    }

    public String getCover() {
        return cover;
    }

    public void setCover(String cover) {
        this.cover = cover;
    }

    public String getPublisher() {
        return publisher;
    }

    public void setPublisher(String publisher) {
        this.publisher = publisher;
    }

    public int getSalesPoint() {
        return salesPoint;
    }

    public void setSalesPoint(int salesPoint) {
        this.salesPoint = salesPoint;
    }

    public boolean isFixedPrice() {
        return fixedPrice;
    }

    public void setFixedPrice(boolean fixedPrice) {
        this.fixedPrice = fixedPrice;
    }

    public int getCustomerReviewRank() {
        return customerReviewRank;
    }

    public void setCustomerReviewRank(int customerReviewRank) {
        this.customerReviewRank = customerReviewRank;
    }

    public SeriesInfo getSeriesInfo() {
        return seriesInfo;
    }

    public void setSeriesInfo(SeriesInfo seriesInfo) {
        this.seriesInfo = seriesInfo;
    }

    public SubInfo getSubInfo() {
        return subInfo;
    }

    public void setSubInfo(SubInfo subInfo) {
        this.subInfo = subInfo;
    }

    @Override
    public String toString() {
        return "BookInfo{" +
                "title='" + title + '\'' +
                ", author='" + author + '\'' +
                ", pubDate='" + pubDate + '\'' +
                ", description='" + description + '\'' +
                ", isbn='" + isbn + '\'' +
                ", isbn13='" + isbn13 + '\'' +
                ", itemId=" + itemId +
                ", priceSales=" + priceSales +
                ", priceStandard=" + priceStandard +
                ", mallType='" + mallType + '\'' +
                ", stockStatus='" + stockStatus + '\'' +
                ", mileage=" + mileage +
                ", cover='" + cover + '\'' +
                ", publisher='" + publisher + '\'' +
                ", salesPoint=" + salesPoint +
                ", fixedPrice=" + fixedPrice +
                ", customerReviewRank=" + customerReviewRank +
                ", seriesInfo=" + seriesInfo +
                ", subInfo=" + subInfo +
                '}';
    }
}
