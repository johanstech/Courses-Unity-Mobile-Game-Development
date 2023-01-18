using System;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;

public class StoreManager : MonoBehaviour, IStoreListener
{
  [SerializeField]
  GameObject restoreButton;
  [SerializeField]
  TMP_Text productTitle;
  [SerializeField]
  TMP_Text productDescription;
  [SerializeField]
  TMP_Text priceText;

  const string NewShipId = "com.johanstech.asteroidavoider.newship";
  public const string NewShipUnlockedKey = "NewShipUnlocked";
  public string Environment = "production";

  IStoreController _controller;
  IExtensionProvider _extensions;

  void Awake()
  {
    if (Application.platform != RuntimePlatform.IPhonePlayer)
    {
      restoreButton.SetActive(false);
    }

    productTitle.text = "Cyan Starship";
    productDescription.text = "Conquer space in style.";
    priceText.text = "$1.99";

    ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
    builder.AddProduct(NewShipId, ProductType.NonConsumable, new IDs
    {
        {"new_ship_google", GooglePlay.Name},
        {"new_ship_mac", MacAppStore.Name}
    });

    UnityPurchasing.Initialize(this, builder);
  }

  async void Start()
  {
    try
    {
      var options = new InitializationOptions().SetEnvironmentName(Environment);
      await UnityServices.InitializeAsync(options);
    }
    catch (Exception e)
    {
      Debug.LogError(e);
    }
  }

  public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
  {
    _controller = controller;
    _extensions = extensions;
  }

  public void OnInitializeFailed(InitializationFailureReason error)
  {
  }

  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
  {
    if (purchaseEvent.purchasedProduct.definition.id == NewShipId)
    {
      PlayerPrefs.SetInt(NewShipUnlockedKey, 1);
    }
    return PurchaseProcessingResult.Complete;
  }

  public void OnPurchaseComplete(Product product)
  {
    if (product.definition.id == NewShipId)
    {
      PlayerPrefs.SetInt(NewShipUnlockedKey, 1);
    }
  }

  public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
  {
    Debug.LogWarning($"Failed to purchase product {product.definition.id}, reason: {reason}");
  }
}
