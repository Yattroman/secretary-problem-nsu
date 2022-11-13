﻿namespace spn.targets.task_4_db.entities;

public class ContenderEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public int Rating { get; set; }
    public int OrderNumber { get; set; }

    public int SearchLoveTryId { get; set; }
    public SearchLoveTry SearchLoveTry { get; set; }

    public ContenderEntity(string firstName, string secondName, int orderNumber, int rating)
    {
        FirstName = firstName;
        SecondName = secondName;
        Rating = rating;
        OrderNumber = orderNumber;
    }
}