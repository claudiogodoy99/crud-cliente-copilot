using Microsoft.Data.SqlClient;
using Model;

namespace Repository
{
    public class ClienteRepository : IClienteRepository, IDisposable
    {
        private readonly SqlConnection _connection;
        private bool _disposed;

        public ClienteRepository()
        {
            _connection = new SqlConnection("");
        }

        public async Task Update(ClienteModel cliente)
        {
            try
            {

                var oldCliente = await Get(cliente.Id);
                if (oldCliente == null) return;

                cliente.Name = cliente.Name ?? oldCliente.Name;
                cliente.Email = cliente.Email ?? oldCliente.Email;
                cliente.Phone = cliente.Phone ?? oldCliente.Phone;

                await _connection.OpenAsync();
                using var command = _connection.CreateCommand();
                command.CommandText = "UPDATE cliente SET name = @Name, email = @Email, phone = @Phone WHERE id = @Id";
                command.Parameters.AddWithValue("@Id", cliente.Id);
                command.Parameters.AddWithValue("@Name", cliente.Name);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Phone", cliente.Phone);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<ClienteModel> Get(string id)
        {
            try
            {
                await _connection.OpenAsync();
                using var command = _connection.CreateCommand();
                command.CommandText = "SELECT * FROM cliente WHERE id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                using var reader = await command.ExecuteReaderAsync();
                await reader.ReadAsync();
                var cliente = new ClienteModel
                {
                    Id = reader.GetInt32(0).ToString(),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phone = reader.GetString(3)
                };
                return cliente;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<ClienteModel>> GetAll()
        {
            try
            {
                await _connection.OpenAsync();
                using var command = _connection.CreateCommand();
                command.CommandText = "SELECT * FROM cliente";
                using var reader = await command.ExecuteReaderAsync();
                var clientes = new List<ClienteModel>();
                while (await reader.ReadAsync())
                {
                    clientes.Add(new ClienteModel
                    {
                        Id = reader.GetInt32(0).ToString(),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        Phone = reader.GetString(3)
                    });
                }
                return clientes;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task Add(ClienteModel cliente)
        {
            try
            {
                await _connection.OpenAsync();
                using var command = _connection.CreateCommand();
                command.CommandText = "INSERT INTO cliente (name, email, phone) VALUES (@Name, @Email, @Phone)";
                command.Parameters.AddWithValue("@Name", cliente.Name);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Phone", cliente.Phone);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                await _connection.OpenAsync();
                using var command = _connection.CreateCommand();
                command.CommandText = "DELETE FROM cliente WHERE id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        // Dispose pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }
                _disposed = true;
            }
        }

    }
}