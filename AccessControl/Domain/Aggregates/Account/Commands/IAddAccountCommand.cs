namespace AccessControl.Domain.Aggregates.Account.Commands;

public interface IAddAccountCommand
{
    string Firstname { get; }
    string Password { get; }
    string Email { get; }
    string Lastname { get; }
}
