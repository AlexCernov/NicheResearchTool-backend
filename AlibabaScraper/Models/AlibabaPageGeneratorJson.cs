using System.Collections.Generic;

namespace AlibabaScraper.Models
{
    public class Bts
    {
        public string magellan_contactsupplier_actiondiff { get; set; }
        public string magellan_new_list_pc { get; set; }
        public string magellan_gallery_refine_compare { get; set; }
        public string magellan_sell_point_style { get; set; }
        public string icbu_excel_bucket { get; set; }
    }

    public class TopTaData
    {
    }

    public class DmTrackView
    {
        public string dmtrackC { get; set; }
        public string dmtrackPageid { get; set; }
    }

    public class PageConfigData
    {
        public bool bannerShow { get; set; }
        public string cookieHistoryTheme { get; set; }
        public string countryId { get; set; }
        public string creditLevelDesc { get; set; }
        public string creditLevelUrl { get; set; }
        public string currentLocation { get; set; }
        public string currentType { get; set; }
        public int displayFXBCategoryId { get; set; }
        public DmTrackView dmTrackView { get; set; }
        public string feedbackTheme { get; set; }
        public string multiTheme { get; set; }
        public string qpShadingTag { get; set; }
        public string quotationHref { get; set; }
        public string requestId { get; set; }
        public string sceneId { get; set; }
        public bool shadeEffective { get; set; }
        public string shadeText { get; set; }
        public string shadeUrl { get; set; }
        public bool showWholesaleModule { get; set; }
        public bool signIn { get; set; }
        public string subscribeHref { get; set; }
        public string surveyUrl { get; set; }
        public string themeType { get; set; }
        public int topDisplayFXBCategoryId { get; set; }
        public bool topicShow { get; set; }
        public string viewtype { get; set; }
        public string virtualExposurePageId { get; set; }
    }

    public class Cluster
    {
        public bool @checked { get; set; }
        public string clusterId { get; set; }
        public string clusterName { get; set; }
        public string href { get; set; }
    }

    public class ClusterListData
    {
        public string clkValue { get; set; }
        public int clusterPos { get; set; }
        public List<Cluster> clusters { get; set; }
        public string cpvAttrItem { get; set; }
        public string exposureValue { get; set; }
    }

    public class CpvClusterData
    {
        public List<ClusterListData> clusterListData { get; set; }
    }

    public class Title
    {
        public string name { get; set; }
    }

    public class Value
    {
        public bool back { get; set; }
        public bool @checked { get; set; }
        public int count { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool noXpjax { get; set; }
    }

    public class MonolayerCategoryData
    {
        public Title title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class Category
    {
        public string flag { get; set; }
        public MonolayerCategoryData monolayerCategoryData { get; set; }
    }

   


    public class CpvFeatureData
    {
        public string checkedNum { get; set; }
        public string clkValue { get; set; }
        public string exposureValue { get; set; }
        public Title title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class Clusters
    {
        public CpvFeatureData cpvFeatureData { get; set; }
    }

    public class ItemList
    {
        public string href { get; set; }
        public bool status { get; set; }
        public string title { get; set; }
    }

    public class ConfigFilter
    {
        public List<ItemList> itemList { get; set; }
    }

   


    public class ExportCountryData
    {
        public Title title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class ExportCountry
    {
        public string clearHref { get; set; }
        public List<ExportCountryData> exportCountryData { get; set; }
    }

   

    public class FreeSampleData
    {
        public Title title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class FreeSample
    {
        public string checkedNum { get; set; }
        public List<FreeSampleData> freeSampleData { get; set; }
    }

    public class MinOrder
    {
        public string clearHref { get; set; }
        public string href { get; set; }
    }

    public class PriceFilter
    {
        public string clearHref { get; set; }
        public string href { get; set; }
    }

    public class Title5
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ProductFeatureData
    {
        public Title5 title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class ProductFeature
    {
        public List<ProductFeatureData> productFeatureData { get; set; }
    }

    public class SelectedResult
    {
        public string clearAllHref { get; set; }
        public string searchText { get; set; }
        public List<object> selectedNodes { get; set; }
    }

   
    public class CompanyAuthTagData
    {
        public Title title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class SnCompanyAuthTagResult
    {
        public string clearHref { get; set; }
        public List<CompanyAuthTagData> companyAuthTagData { get; set; }
    }



    public class ProductAuthTagData
    {
        public Title title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class SnProductAuthTagResult
    {
        public string clearHref { get; set; }
        public List<ProductAuthTagData> productAuthTagData { get; set; }
    }

  

    public class SnPromotion2
    {
        public string clearAllHref { get; set; }
        public Title title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class SnPromotion
    {
        public SnPromotion2 snPromotion { get; set; }
    }

    public class CountrySupplierLocation
    {
        public bool back { get; set; }
        public bool @checked { get; set; }
        public int count { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool noXpjax { get; set; }
        public List<object> childs { get; set; }
    }

    public class SuggestedSupplierLocationData
    {
        public bool back { get; set; }
        public bool @checked { get; set; }
        public int count { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool noXpjax { get; set; }
    }

    public class SupplierLocationData
    {
        public Title title { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class SupplierLocation
    {
        public string allCountryHref { get; set; }
        public string checkedNum { get; set; }
        public string cleanAllLink { get; set; }
        public List<CountrySupplierLocation> countrySupplierLocation { get; set; }
        public bool needCountryDirect { get; set; }
        public bool needCountryGuide { get; set; }
        public List<SuggestedSupplierLocationData> suggestedSupplierLocationData { get; set; }
        public List<SupplierLocationData> supplierLocationData { get; set; }
    }

    public class Title10
    {
        public string name { get; set; }
    }

    public class Value9
    {
        public bool back { get; set; }
        public bool @checked { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool noXpjax { get; set; }
    }

    public class SupplierTypeData
    {
        public Title10 title { get; set; }
        public string type { get; set; }
        public List<Value9> values { get; set; }
    }

    public class SupplierType
    {
        public string checkedNum { get; set; }
        public List<SupplierTypeData> supplierTypeData { get; set; }
    }

    public class SnData
    {
        public Category category { get; set; }
        public Clusters clusters { get; set; }
        public ConfigFilter configFilter { get; set; }
        public ExportCountry exportCountry { get; set; }
        public FreeSample freeSample { get; set; }
        public MinOrder minOrder { get; set; }
        public PriceFilter priceFilter { get; set; }
        public ProductFeature productFeature { get; set; }
        public SelectedResult selectedResult { get; set; }
        public SnCompanyAuthTagResult snCompanyAuthTagResult { get; set; }
        public SnProductAuthTagResult snProductAuthTagResult { get; set; }
        public SnPromotion snPromotion { get; set; }
        public SupplierLocation supplierLocation { get; set; }
        public SupplierType supplierType { get; set; }
    }

    public class Word
    {
        public bool activity { get; set; }
        public string href { get; set; }
        public string name { get; set; }
    }

    public class RelatedResult
    {
        public int displayNum { get; set; }
        public List<Word> words { get; set; }
    }

    public class BottomRelatedSearchData
    {
        public RelatedResult relatedResult { get; set; }
    }

    public class BottomP4P
    {
        public string advlang { get; set; }
        public string avVmVerifiedFlag { get; set; }
        public string businessType { get; set; }
        public string clickServerDomain { get; set; }
        public int count { get; set; }
        public string country { get; set; }
        public string currentLang { get; set; }
        public string custType { get; set; }
        public string dcatid { get; set; }
        public string dl { get; set; }
        public string domain { get; set; }
        public string exportMarket { get; set; }
        public string imgSizeRule { get; set; }
        public string ip { get; set; }
        public string keyword { get; set; }
        public string mt { get; set; }
        public string offertype { get; set; }
        public int offset { get; set; }
        public int pageIndex { get; set; }
        public string pid { get; set; }
        public string propertyValueIds { get; set; }
        public string server { get; set; }
        public bool showIntelMatch { get; set; }
        public string styleVersion { get; set; }
        public string testVersion { get; set; }
        public string urlPrefix { get; set; }
    }

    public class ListP4P
    {
        public string custId { get; set; }
        public string id { get; set; }
        public string pid { get; set; }
    }

    public class SideP4P
    {
        public string advlang { get; set; }
        public string avVmVerifiedFlag { get; set; }
        public string businessType { get; set; }
        public string clickServerDomain { get; set; }
        public int count { get; set; }
        public string country { get; set; }
        public string currentLang { get; set; }
        public string custType { get; set; }
        public string dcatid { get; set; }
        public string dl { get; set; }
        public string domain { get; set; }
        public string exportMarket { get; set; }
        public string imgSizeRule { get; set; }
        public string ip { get; set; }
        public string keyword { get; set; }
        public string mt { get; set; }
        public string offertype { get; set; }
        public int offset { get; set; }
        public int pageIndex { get; set; }
        public string pid { get; set; }
        public string propertyValueIds { get; set; }
        public string server { get; set; }
        public bool showIntelMatch { get; set; }
        public string styleVersion { get; set; }
        public string testVersion { get; set; }
        public string urlPrefix { get; set; }
    }

    public class P4pData
    {
        public BottomP4P bottomP4P { get; set; }
        public ListP4P listP4P { get; set; }
        public SideP4P sideP4P { get; set; }
    }

    public class ForbidData
    {
        public bool dangerous { get; set; }
        public bool forbidden { get; set; }
        public bool forbiddenSell { get; set; }
    }

    public class SortBy
    {
        public bool @checked { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
    }

    public class SortByVO
    {
        public List<SortBy> sortBy { get; set; }
    }

    public class ViewA
    {
        public bool @checked { get; set; }
        public string href { get; set; }
        public string type { get; set; }
    }

    public class ViewAsVO
    {
        public bool newGallery { get; set; }
        public bool oldToNewGallery { get; set; }
        public List<ViewA> viewAs { get; set; }
    }

    public class SwitchData
    {
        public SortByVO sortByVO { get; set; }
        public ViewAsVO viewAsVO { get; set; }
    }

    public class AliTalk
    {
        public string status { get; set; }
        public string tmlid { get; set; }
    }

    public class Transaction
    {
        public string conducted { get; set; }
        public string num { get; set; }
    }

    public class Record
    {
        public string responseRate { get; set; }
        public string responseTime { get; set; }
        public Transaction transaction { get; set; }
    }

    public class Company
    {
        public string bizType { get; set; }
        public bool governmentRec { get; set; }
        public string mainExportProducts { get; set; }
        public string otAvgLeadTime { get; set; }
        public string otMainCustDesc { get; set; }
        public string otTotalOrdCnt { get; set; }
        public string rdAbilityAssessed { get; set; }
        public Record record { get; set; }
        public string selectionHref { get; set; }
        public string selectionViewAllHref { get; set; }
        public string supplierHistoryGmv { get; set; }
        public int supplierHistoryOrderCount { get; set; }
        public string tradeAssurance { get; set; }
        public int transactionLevel { get; set; }
        public double transactionLevelFloat { get; set; }
        public string transactionLevelUrl { get; set; }
        public string expCountry { get; set; }
        public string certAssessed { get; set; }
        public string compMgtCertAssessed { get; set; }
    }

    public class ProductAttribute
    {
        public string name { get; set; }
        public string originalName { get; set; }
        public string value { get; set; }
        public int? id { get; set; }
        public int? valueId { get; set; }
    }

    public class Features
    {
        public bool crossReference { get; set; }
        public List<ProductAttribute> productAttribute { get; set; }
        public List<string> tagAescList { get; set; }
    }

    public class Image
    {
        public string extendImage { get; set; }
        public string mainImage { get; set; }
        public List<string> multiImage { get; set; }
        public string productImage { get; set; }
        public bool video { get; set; }
        public string videoId { get; set; }
        public string videoPath { get; set; }
    }

    public class Information
    {
        public string activityTags { get; set; }
        public bool ad { get; set; }
        public bool adP4p { get; set; }
        public string buyNow { get; set; }
        public bool bwSimilarProductDisplay { get; set; }
        public bool complaint { get; set; }
        public List<int> displayCatIds { get; set; }
        public bool edm { get; set; }
        public string encryptId { get; set; }
        public string eurl { get; set; }
        public int grade { get; set; }
        public bool i2I { get; set; }
        public object id { get; set; }
        public bool inq { get; set; }
        public string ladderPeriod { get; set; }
        public bool lyb { get; set; }
        public bool mainProduct { get; set; }
        public bool market { get; set; }
        public int minProcessPeriod { get; set; }
        public bool newAd { get; set; }
        public bool oneTouch { get; set; }
        public bool p4p { get; set; }
        public bool personalizeOffer { get; set; }
        public int postCategoryId { get; set; }
        public string productUrl { get; set; }
        public string puretitle { get; set; }
        public string rankScoreInfo { get; set; }
        public string selectionDetailHref { get; set; }
        public bool signedIn { get; set; }
        public string title { get; set; }
        public string similarProduct { get; set; }
    }

    public class PromotionInfoVO
    {
        public string localOriginalPriceRangeStr { get; set; }
        public string originalPriceFrom { get; set; }
        public string originalPriceTo { get; set; }
        public List<object> quantityPrices { get; set; }
        public bool showHalfOff { get; set; }
        public bool showPromotion { get; set; }
    }

    public class Review
    {
        public int count { get; set; }
        public string reviewContent { get; set; }
        public string reviewLink { get; set; }
        public string tagId { get; set; }
    }

    public class Reviews
    {
        public string productScore { get; set; }
        public int reviewCount { get; set; }
        public string reviewLink { get; set; }
        public string reviewScore { get; set; }
        public List<Review> reviews { get; set; }
        public string scoreText { get; set; }
        public string shippingTime { get; set; }
        public string supplierService { get; set; }
        public string productReviewScore { get; set; }
    }

    public class SupplierCountry
    {
        public string id { get; set; }
        public string name { get; set; }
        public string searchOverHref { get; set; }
    }

    public class SupplierProvince
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Supplier
    {
        public bool assessedSupplier { get; set; }
        public string companyLogo { get; set; }
        public string contactSupplier { get; set; }
        public string encryptSupplierId { get; set; }
        public bool goldSupplier { get; set; }
        public string provideProducts { get; set; }
        public SupplierCountry supplierCountry { get; set; }
        public string supplierHref { get; set; }
        public int supplierId { get; set; }
        public string supplierName { get; set; }
        public string supplierProductListHref { get; set; }
        public SupplierProvince supplierProvince { get; set; }
        public string supplierYear { get; set; }
    }

    public class Tag
    {
        public string companyAuthProvider { get; set; }
        public List<object> productTag { get; set; }
        public List<object> supplierTag { get; set; }
        public string tag { get; set; }
        public List<string> companyAuthTag { get; set; }
    }

    public class TradePrice
    {
        public string minOrder { get; set; }
        public string price { get; set; }
        public string priceMini { get; set; }
        public string unit { get; set; }
    }

    public class OfferList
    {
        public AliTalk aliTalk { get; set; }
        public Company company { get; set; }
        public Features features { get; set; }
        public object id { get; set; }
        public Image image { get; set; }
        public Information information { get; set; }
        public PromotionInfoVO promotionInfoVO { get; set; }
        public Reviews reviews { get; set; }
        public Supplier supplier { get; set; }
        public Tag tag { get; set; }
        public TradePrice tradePrice { get; set; }
    }

    public class SpuResult
    {
        public int totalCount { get; set; }
    }

    public class ThemeUrl
    {
        public int position { get; set; }
        public string themeurl { get; set; }
    }

    public class OfferResultData
    {
        public string activityConfig { get; set; }
        public int adCount { get; set; }
        public bool asyncGallery { get; set; }
        public string asyncRequestUrl { get; set; }
        public bool firstScreen { get; set; }
        public int insertRfqForm { get; set; }
        public List<OfferList> offerList { get; set; }
        public int p4pCount { get; set; }
        public string pagingUrl { get; set; }
        public string preferOfferUrl { get; set; }
        public bool rightp4poff { get; set; }
        public SpuResult spuResult { get; set; }
        public List<ThemeUrl> themeUrls { get; set; }
        public int totalCount { get; set; }
    }

    public class Params
    {
        public string os { get; set; }
        public string ecredit { get; set; }
        public string chinasuppliers { get; set; }
        public string merge_supplier { get; set; }
        public string language { get; set; }
        public string onsite { get; set; }
        public string SearchText { get; set; }
        public string n { get; set; }
        public string need_cd { get; set; }
        public string assessment_company { get; set; }
        public string searchweb { get; set; }
        public string ggs { get; set; }
        public string av { get; set; }
        public string needTranslate { get; set; }
        public string escrow { get; set; }
        public string ctrPageId { get; set; }
        public string atm { get; set; }
        public string page { get; set; }
    }

    public class RequestData
    {
        public bool canWebp { get; set; }
        public string charset { get; set; }
        public Params @params { get; set; }
        public string serverUri { get; set; }
        public string servletPath { get; set; }
    }

    public class Props
    {
        public Bts bts { get; set; }
        public TopTaData topTaData { get; set; }
        public PageConfigData pageConfigData { get; set; }
        public CpvClusterData cpvClusterData { get; set; }
        public SnData snData { get; set; }
        public int selectedNodeSize { get; set; }
        public BottomRelatedSearchData bottomRelatedSearchData { get; set; }
        public P4pData p4pData { get; set; }
        public ForbidData forbidData { get; set; }
        public int offerTotalCount { get; set; }
        public SwitchData switchData { get; set; }
        public OfferResultData offerResultData { get; set; }
        public RequestData requestData { get; set; }
        public string keyword { get; set; }
    }

    public class AlibabaPageGeneratorJson
    {
        public string app { get; set; }
        public string loader { get; set; }
        public string module { get; set; }
        public string version { get; set; }
        public List<string> commons { get; set; }
        public Props props { get; set; }
    }
  
}
