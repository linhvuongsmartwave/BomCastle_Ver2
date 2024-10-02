using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.Purchasing.Extension;
using TMPro;





public class IAPManager : Singleton<IAPManager>, IDetailedStoreListener
{
    private List<ItemIAP> listItems = new List<ItemIAP>();
    public TextMeshProUGUI txtNoPackage;
    //public TextMeshProUGUI txtRuby;
    public GameObject successFully;
    const string PACK_1 = "com.boomcastle_v1.pack1";
    const string PACK_2 = "com.boomcastle_v1.pack2";
    const string PACK_3 = "com.boomcastle_v1.pack3";
    const string PACK_4 = "com.boomcastle_v1.pack4";
    const string PACK_5 = "com.boomcastle_v1.pack5";
    const string PACK_6 = "com.boomcastle_v1.pack6";
    const string PACK_7 = "com.boomcastle_v1.pack7";
    const string PACK_8 = "com.boomcastle_v1.pack8";


    void Start()
    {
        successFully.SetActive(false);
    }
    IStoreController m_StoreController;
    int numberHint;

    //SETUP BUILDER
    public void SetupBuilder()
    {
        listItems.Add(new ItemIAP(PACK_1, "0"));
        listItems.Add(new ItemIAP(PACK_2, "0"));
        listItems.Add(new ItemIAP(PACK_3, "0"));
        listItems.Add(new ItemIAP(PACK_4, "0"));
        listItems.Add(new ItemIAP(PACK_5, "0"));
        listItems.Add(new ItemIAP(PACK_6, "0"));
        listItems.Add(new ItemIAP(PACK_7, "0"));
        listItems.Add(new ItemIAP(PACK_8, "0"));

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (var item in listItems)
        {
            builder.AddProduct(item.id, ProductType.Consumable);
        }
        Debug.Log("SetupBuilder");
        var catalog = ProductCatalog.LoadDefaultCatalog();
        UnityPurchasing.Initialize(this, builder);
    }

    /** UI BUTTON EVENTs for PURCHASE **/
    public void HandleInitiatePurchase(int posPack)
    {
        String productId = PACK_1;
        switch (posPack)
        {
            case 2:
                productId = PACK_2;
                break;
            case 3:
                productId = PACK_3;
                break;
            case 4:
                productId = PACK_4;
                break;
            case 5:
                productId = PACK_5;
                break;
            case 6:
                productId = PACK_6;
                break;
            case 7:
                productId = PACK_7;
                break;
            case 8:
                productId = PACK_8;
                break;
        }
        SetHandlePurchase(productId);
    }

    private void SetHandlePurchase(String pack)
    {
        m_StoreController.InitiatePurchase(pack);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("false");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        var hint = 5;
        switch (product.definition.id)
        {
            case PACK_2:
                hint = 10;
                break;
            case PACK_3:
                hint = 15;
                break;
            case PACK_4:
                hint = 20;
                break;
            case PACK_5:
                hint = 30;
                break;
            case PACK_6:
                hint = 40;
                break;
            case PACK_7:
                hint = 50;
                break;
            case PACK_8:
                hint = 100;
                break;
        }
        Shop.Instance.BuyGold(hint);
        successFully.SetActive(true);
        AudioManager.Instance.AudioCoin();


        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Init purchase success!");
        List<int> ListPosPack = new List<int>();
        m_StoreController = controller;
        for (int i = 0; i < listItems.Count; i++)
        {
            Product product = m_StoreController.products.WithID(listItems[i].id);
            if (product != null && product.metadata != null)
            {
                Debug.Log($"Product ID: {listItems[i].id} - Price: {product.metadata.localizedPriceString}");
                listItems[i].price = product.metadata.localizedPriceString;
                ListPosPack.Add(i + 1);

            }

        }

        ShopManager.Instance.SetLayoutItemIAP(listItems, ListPosPack);
        txtNoPackage.gameObject.SetActive(false);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("false1");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new NotImplementedException();
    }
}
