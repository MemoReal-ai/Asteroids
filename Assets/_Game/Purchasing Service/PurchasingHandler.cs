using UnityEngine;

namespace _Game.Purchasing_Service
{
    public class PurchasingHandler : IPurchasingService
    {
        
        public void Buy()
        {
            Debug.Log("Purchasing service buy");
        }
    }
}