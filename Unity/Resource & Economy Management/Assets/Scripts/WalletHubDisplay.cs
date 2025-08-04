using UnityEngine;
using TMPro; // Use TextMeshPro for modern, flexible text rendering

/// <summary>
/// Manages the display of the main Wallet Hub UI.
/// Reads wallet data from a JSON file and updates the corresponding text fields.
/// </summary>
public class WalletHubDisplay : MonoBehaviour
{

    #region Data Structures for JSON Parsing

    [System.Serializable]
    public class WalletData
    {
        public int coins;
        public int gems;
        public int credits;
    }

    [System.Serializable]
    public class ForecastData
    {
        public int net_delta;
    }

    [System.Serializable]
    public class WalletDataPayload
    {
        public WalletData wallet;
        public ForecastData forecast;
    }

    #endregion

    #region UI References

    [Header("UI Text Elements")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI forecastText;

    #endregion

    void Start()
    {

        TextAsset jsonFile = Resources.Load<TextAsset>("data");

        if (jsonFile != null)
        {
            UpdateDisplay(jsonFile.text);
        }
        else
        {
            Debug.LogError("Failed to load 'wallet_hub_data.json'. Make sure it's in a 'Resources' folder.");
        }
    }


    public void UpdateDisplay(string jsonData)
    {
  
        WalletDataPayload data = JsonUtility.FromJson<WalletDataPayload>(jsonData);

        if (data == null)
        {
            Debug.LogError("Failed to parse Wallet JSON data.");
            return;
        }

        coinsText.text = "Coins : " + data.wallet.coins.ToString("N0");
        gemsText.text = "Gems : " + data.wallet.gems.ToString("N0");
        creditsText.text = "Coaching Credit : " + data.wallet.credits.ToString("N0");

        if (data.forecast.net_delta >= 0)
        {
            forecastText.text = $"Weekly Forecast: <color=green> +{data.forecast.net_delta:N0}</color> Coins";
        }
        else
        {
            forecastText.text = $"Weekly Forecast: <color=red> {data.forecast.net_delta:N0}</color> Coins";
        }
    }
}
