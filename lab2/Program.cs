using Microsoft.Data.SqlClient;

namespace p4_lab2
{
    internal class Program
    {
        private static string connectionString = "Data Source=DESKTOP-VAP0IFG\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Trust Server Certificate=True";
        static void Main(string[] args)
        {
            //// wypisanie listy z wszystkimi klientami 

            //var customers = GetAllCustomers();

            //int liczbaKlientow = 0;
            //foreach (var customer in customers)
            //{
            //    Console.WriteLine(new string('-', 50));
            //    Console.WriteLine($"ID: {customer.CustomerID}");
            //    Console.WriteLine($"Company Name: {customer.CompanyName}");
            //    Console.WriteLine($"Contact Name: {customer.ContactName}");
            //    Console.WriteLine($"Contact Title: {customer.ContactTitle}");
            //    Console.WriteLine($"Address: {customer.Address}");
            //    Console.WriteLine($"City: {customer.City}");
            //    Console.WriteLine($"Region: {customer.Region}");
            //    Console.WriteLine($"Postal Code: {customer.PostalCode}");
            //    Console.WriteLine($"Country: {customer.Country}");
            //    Console.WriteLine($"Phone: {customer.Phone}");
            //    Console.WriteLine($"Fax: {customer.Fax}");
            //    Console.WriteLine(new string('-', 50));
            //    liczbaKlientow++;
            //}
            //Console.WriteLine("Liczba klientow: " + liczbaKlientow);
            //Console.WriteLine();



            //// dodanie nowego klienta

            //var newCustomer = new Customer
            //{
            //    CustomerID = "JKJKK", 
            //    CompanyName = "Firma", 
            //    ContactName = "Jan Kowalski", 
            //};

            //var id = AddCustomer(newCustomer);
            //Console.WriteLine($"Nowy klient został dodany z CustomerID: {id}");



            // aktualizacja danych klienta

            //var customerNewData = new Customer
            //{
            //    CustomerID = "JKJKK", 
            //    CompanyName = "Nowa Firma", 
            //    ContactName = "Jan Janusz Kowalski" 
            //};

            //UpdateCustomer(customerNewData);
            //Console.WriteLine("Dane klienta zostały zaktualizowane.");



            //// wyszukiwanie klienta

            //Console.Write("Podaj ID klienta: ");
            //string idCustomer = Console.ReadLine();

            //var customer = GetCustomerById(idCustomer);

            //if (customer != null)
            //{
            //    Console.WriteLine($"ID: {customer.CustomerID}");
            //    Console.WriteLine($"Company Name: {customer.CompanyName}");
            //    Console.WriteLine($"Contact Name: {customer.ContactName}");
            //}
            //else
            //{
            //    Console.WriteLine("Klient nie istnieje.");
            //}



            //// usuwanie klienta

            //Console.Write("Podaj ID klienta do usunięcia: ");
            //string idCustomer = Console.ReadLine();

            //RemoveCustomerById(idCustomer);

        }
        public static List<Customer> GetAllCustomers()  // funkcja pobierajaca klientow i zwracajaca liste obiektow
        {
            var querySqlI = "SELECT * FROM Customers";
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(querySqlI, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer
                            {
                                CustomerID = reader["CustomerID"] as string,
                                CompanyName = reader["CompanyName"] as string,
                                ContactName = reader["ContactName"] as string,
                                ContactTitle = reader["ContactTitle"] as string,
                                Address = reader["Address"] as string,
                                City = reader["City"] as string,
                                Region = reader["Region"] as string,
                                PostalCode = reader["PostalCode"] as string,
                                Country = reader["Country"] as string,
                                Phone = reader["Phone"] as string,
                                Fax = reader["Fax"] as string,
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }

        public static string AddCustomer(Customer customer) // funkcja dodajaca nowego klienta do bazy danych
        {
            string newCustomerId = customer.CustomerID;  
            string query = "INSERT INTO Customers (CustomerID, CompanyName, ContactName) VALUES (@CustomerID, @CompanyName, @ContactName);";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                    command.Parameters.AddWithValue("@ContactName", customer.ContactName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return newCustomerId;
        }

        public static void UpdateCustomer(Customer customer) // funkcja aktualizujaca dane klienta 
        {
            string query = "UPDATE Customers SET CompanyName = @CompanyName, ContactName = @ContactName WHERE CustomerID = @CustomerID;";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                    command.Parameters.AddWithValue("@ContactName", customer.ContactName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static Customer GetCustomerById(string customerId) // funkcja pobierajaca dane klienta o wskazanym id
        {
            string query = "SELECT * FROM Customers WHERE CustomerID = @CustomerID;";
            Customer customer = null;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer
                            {
                                CustomerID = reader["CustomerID"] as string,
                                CompanyName = reader["CompanyName"] as string,
                                ContactName = reader["ContactName"] as string,
                                ContactTitle = reader["ContactTitle"] as string,
                                Address = reader["Address"] as string,
                                City = reader["City"] as string,
                                Region = reader["Region"] as string,
                                PostalCode = reader["PostalCode"] as string,
                                Country = reader["Country"] as string,
                                Phone = reader["Phone"] as string,
                                Fax = reader["Fax"] as string,
                            };
                        }
                    }
                }
            }

            return customer;
        }

        public static void RemoveCustomerById(string customerId) // funkcja usuwajaca klienta o wskazanym id
        {
            string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID;";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerId);
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Klient o ID {customerId} został usunięty.");
                    }
                    else
                    {
                        Console.WriteLine($"Klient o ID {customerId} nie istnieje.");
                    }
                }
            }
        }

    }
}
