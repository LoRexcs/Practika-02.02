using System;
using System.Collections.Generic;

class Appointment
{
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string PatientLastName { get; set; }
}

class Schedule
{
    private List<Appointment> appointments = new List<Appointment>();

    public void AddAppointment()
    {
        Console.WriteLine("Введите дату (в формате дд.мм.гггг):");
        DateTime date;
        if (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.WriteLine("Некорректный формат даты. Попробуйте снова.");
            return;
        }

        Console.WriteLine("Введите время (в формате чч:мм):");
        TimeSpan time;
        if (!TimeSpan.TryParse(Console.ReadLine(), out time))
        {
            Console.WriteLine("Некорректный формат времени. Попробуйте снова.");
            return;
        }

        Console.WriteLine("Введите фамилию пациента:");
        string patientLastName = Console.ReadLine();

        var appointment = new Appointment
        {
            Date = date,
            Time = time,
            PatientLastName = patientLastName
        };

        if (!IsTimeSlotOccupied(appointment.Time))
        {
            appointments.Add(appointment);
            Console.WriteLine("Запись успешно добавлена.");
        }
        else
        {
            Console.WriteLine("Это время уже занято. Выберите другое.");
        }
    }

    public void PrintSchedule()
    {
        foreach (var appointment in appointments)
        {
            Console.WriteLine($"{appointment.Date.ToShortDateString()} {appointment.Time} - {appointment.PatientLastName}");
        }
    }

    private bool IsTimeSlotOccupied(TimeSpan time)
    {
        foreach (var appointment in appointments)
        {
            if (appointment.Time == time)
            {
                return true;
            }
        }
        return false;
    }

    // Другие методы, такие как удаление записи, сохранение в файл и т.д.
}

class Program
{
    static void Main()
    {
        Schedule schedule = new Schedule();

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить запись");
            Console.WriteLine("2. Вывести расписание");
            Console.WriteLine("3. Выйти");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    schedule.AddAppointment();
                    break;
                case 2:
                    schedule.PrintSchedule();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}
