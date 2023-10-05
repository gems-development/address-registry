using Gems;
using Gems.AddressRegistry.ApplicationServices;
using Gems.AddressRegistry.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        DataImportService dis =new DataImportService();
        ERN[] eRNmas = new ERN[2];
        eRNmas[0] = new ERN();
        eRNmas[1] = new ERN();
        dis.ERNImport(eRNmas);

    }
}