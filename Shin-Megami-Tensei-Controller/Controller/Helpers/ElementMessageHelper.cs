namespace Shin_Megami_Tensei;

public static class ElementMessageHelper
{
    public static string GetElementalMessage(AffinityElement element)
    {
        return element switch
        {
            AffinityElement.Physical => "ataca",
            AffinityElement.Gun => "dispara",
            AffinityElement.Fire => "lanza fuego",
            AffinityElement.Ice => "lanza hielo",
            AffinityElement.Elec => "lanza electricidad",
            AffinityElement.Force => "lanza viento",
            AffinityElement.Light => "ataca con luz",
            AffinityElement.Dark => "ataca con oscuridad",
            _ => "usa una habilidad"
        };
    }
}