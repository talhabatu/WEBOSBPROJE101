public class FaturaResultComparer : IEqualityComparer<FaturaResult>
{
    public bool Equals(FaturaResult x, FaturaResult y)
    {
        return x.FATURANO == y.FATURANO;  
    }

    public int GetHashCode(FaturaResult obj)
    {
        return obj.FATURANO.GetHashCode();  
    }
}
