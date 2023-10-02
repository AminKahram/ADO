
using ADOSamples.ConsoleUI;

//SqlSample.FirstSample();
//------------------------------------------------------

//SqlSample.WorkingWithConnection();

//----------------------------------------------------

//SqlSample.ConnectionBuilder();

//----------------------------------------------------

SqlSample sqlSample = new SqlSample();
//sqlSample.TestCommand();

//----------------------------------------------------

//sqlSample.TestReader();

//----------------------------------------------------

//sqlSample.TestReaderMultiple();

//----------------------------------------------------

//sqlSample.AddProduct(1, "Chomagh", "Chomagh description", 25000);

//----------------------------------------------------

//sqlSample.AddProductParameter(2, "Abbas", "Abbasi Description" , 2000000000);

//----------------------------------------------------

//sqlSample.AddTransactional("tablue".PadLeft(990000000,'-'), 2, "Monaliza3", "Monaliza3 zhecond laugh", 222222222);

//----------------------------------------------------

//sqlSample.SimpleBulkInsert();
sqlSample.SimpleBulkCopyInsert();

//----------------------------------------------------


Console.ReadKey();