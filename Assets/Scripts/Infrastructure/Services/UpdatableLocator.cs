using UnityEngine;

internal class UpdatableLocator : MonoBehaviour
{
    private UpdatableServices _updatableServices;
    
    public void SetUpdateableServices(UpdatableServices updatableServices)
    {
        _updatableServices = updatableServices;
    }

    private void FixedUpdate()
    {
        _updatableServices?.FixedUpdate();
    }
}