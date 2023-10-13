using System;
using System.Collections.Generic;


namespace Characters
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите количество персонажей: ");
            int amount_сhars = int.Parse(Console.ReadLine());
            List<game_character> characters = new List<game_character>();
            // Создаем список персонажей
            for (int i = 0; i < amount_сhars; i++)
            {
                string name = $"Персонаж{i + 1}";
                int x = new Random().Next(1, 16); // Случайная координата X от 1 до 15
                int y = new Random().Next(1, 16); // Случайная координата Y от 1 до 15
                bool side = new Random().Next(2) == 0; // Случайно выбираем сторону (true или false)
                int hp = 100; // Измените, если нужно другое начальное здоровье
                bool alive = true; // Новые персонажи живы

                game_character newCharacter = new game_character();
                newCharacter.new_chars(name, x, y, side, hp, alive);
                characters.Add(newCharacter);
            }


            while (true)
            {
                Console.WriteLine("1. Показать информацию о всех персонажах");
                Console.WriteLine("2. Создать нового персонажа");
                Console.WriteLine("3. Удалить персонажа");
                Console.WriteLine("4. Воскресить персонажа");
                Console.WriteLine("5. Действия персонажа");
                //добавть проверку на то жив ли перс или нет

                ConsoleKeyInfo keyInfo0 = Console.ReadKey(true);

                switch (keyInfo0.Key)
                {
                    case ConsoleKey.D1:

                        Console.Clear();
                        game_character.show_all_charsinfo();
                        Console.WriteLine();
                        break;

                    case ConsoleKey.D2:

                        Console.Clear();
                        new_char1(characters);
                        break;

                    case ConsoleKey.D3:

                        Console.Clear();
                        if (characters.Count != 0)
                        {
                            delete_char(characters);
                        }
                        else
                        {
                            Console.WriteLine("Нет доступных персонажей для удаления\n");
                        }

                        break;

                    case ConsoleKey.D4:

                        Console.Clear();
                        if (characters.Count != 0)
                        {
                            rebirth_char(characters);
                        }
                        else
                        {
                            Console.WriteLine("Нет доступных персонажей для воскрешения\n");
                        }

                        break;

                    case ConsoleKey.D5:

                        Console.Clear();
                        if (characters.Count != 0)
                        {
                            account_actions(characters);
                        }
                        else
                        {
                            Console.WriteLine("Нет доступных персонажей, необходимо создать нового\n");
                        }

                        break;

                }
            }
        }

        static void account_actions(List<game_character> characters)
        {
            game_character.show_all_chars(characters);

            Console.Write("\nВыберите персонажа: ");
            int choice = (int.Parse(Console.ReadLine()) - 1);
            int distance = 0;
            bool bebra = true;
            if (characters[choice].get_alive())
            {
                if (choice >= 0 && choice < characters.Count)
                {
                    Console.Clear();
                    while (bebra)
                    {
                        characters[choice].info(true);

                        Console.WriteLine("\n1. Идти вверх ");
                        Console.WriteLine("2. Идти вниз");
                        Console.WriteLine("3. Идти влево");
                        Console.WriteLine("4. Идти вправо");
                        Console.WriteLine("5. Атаковать");
                        Console.WriteLine("6. Лечиться/лечить");
                        Console.WriteLine("Q. Админ меню");
                        Console.WriteLine("E. Вернуться назад");

                        ConsoleKeyInfo keyInfo1 = Console.ReadKey(true);

                        switch (keyInfo1.Key)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                Console.Write("Введите дистанцию: ");
                                distance = int.Parse(Console.ReadLine());
                                characters[choice].move_up(distance);
                                Console.WriteLine();
                                break;

                            case ConsoleKey.D2:
                                Console.Clear();
                                Console.Write("Введите дистанцию: ");
                                distance = int.Parse(Console.ReadLine());
                                characters[choice].move_down(distance);
                                Console.WriteLine();
                                break;

                            case ConsoleKey.D3:
                                Console.Clear();
                                Console.Write("Введите дистанцию: ");
                                distance = int.Parse(Console.ReadLine());
                                characters[choice].move_left(distance);
                                Console.WriteLine();
                                break;

                            case ConsoleKey.D4:
                                Console.Clear();
                                Console.Write("Введите дистанцию: ");
                                distance = int.Parse(Console.ReadLine());
                                characters[choice].move_right(distance);
                                Console.WriteLine();
                                break;

                            case ConsoleKey.D5: // атака
                                Console.Clear();
                                atack_char(characters, choice);
                                break;

                            case ConsoleKey.D6: // лечение
                                Console.Clear();
                                healing_char(characters);
                                break;

                            case ConsoleKey.Q: // админ меню
                                Console.Clear();
                                admin_menu(characters, choice);
                                break;

                            case ConsoleKey.E:
                                Console.Clear();
                                bebra = false;
                                break;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неверный выбор персонажа.\n");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Этот персонаж мертв.\n");
            }
        }

        static void admin_menu(List<game_character> characters, int choice)
        {
            Console.Clear();
            bool a = true;
            while (a)
            {
                characters[choice].info(true);

                Console.WriteLine("\n1. Убить персонажа");
                Console.WriteLine("2. Вылечить персонажа");
                Console.WriteLine("3. Сменить фракцию");
                Console.WriteLine("4. Вернуться назад");

                ConsoleKeyInfo keyInfo2 = Console.ReadKey(true);

                switch (keyInfo2.Key)
                {
                    case ConsoleKey.D1:

                        Console.Clear();
                        characters[choice].kill();
                        Console.WriteLine();
                        break;

                    case ConsoleKey.D2:

                        Console.Clear();
                        characters[choice].full_heal();
                        Console.WriteLine($"Персонаж {characters[choice]} вылечен");
                        Console.WriteLine();
                        break;

                    case ConsoleKey.D3:

                        Console.Clear();
                        characters[choice].change_side();
                        Console.WriteLine();
                        break;

                    case ConsoleKey.D4:

                        Console.Clear();
                        a = false;
                        Console.WriteLine();
                        break;

                }
            }

        }

        static void new_char1(List<game_character> characters)
        {
            Console.Write("Введите имя персонажа: ");
            string name = Console.ReadLine();
            characters.Add(new game_character());
            characters[characters.Count - 1].new_chars(name, 0, 0, true, 100, true);
            Console.Clear();
            Console.WriteLine("Персонаж создан!\n");
        }

        static void delete_char(List<game_character> characters)
        {
            game_character.show_all_chars(characters);

            Console.Write("\nВыберите персонажа, которого хотите удалить: ");
            int c = (int.Parse(Console.ReadLine()) - 1);
            if (c > 0 && c <= characters.Count)
            {
                Console.Clear();
                string deleted_name = characters[c].get_name();
                characters[c - 1].delete_account1(c);
                characters.RemoveAt(c);
                Console.WriteLine($"Персонаж {deleted_name} удален\n");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Неверный номер персонажа.");
            }
        }





        static void rebirth_char(List<game_character> characters)
        {
            while (true)
            {
                game_character.show_all_dead();

                Console.Write("\nВыберите персонажа, которого хотите воскресить: ");
                int c = (int.Parse(Console.ReadLine()) - 1);
                if (c >= 0 && c < characters.Count && !characters[c].get_alive())
                {
                    characters[c].rebirth();
                    break; // Выход из цикла после успешного возрождения
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неверный номер персонажа или персонаж уже жив.");
                }
            }

        }

        static void atack_char(List<game_character> characters, int choice)
        {
            Console.Clear();
            game_character.show_all_chars(characters);

            Console.Write("\nВыберите цель для атаки: ");
            int targetChoice = int.Parse(Console.ReadLine()) - 1;

            if (targetChoice >= 0 && targetChoice < characters.Count && characters[targetChoice].get_alive())
            {
                characters[choice].atack(targetChoice); // Передаем только индекс цели
            }
            else
            {
                Console.WriteLine("Неверный выбор цели.");
            }

            Console.WriteLine();
        }

        static void healing_char(List<game_character> characters)
        {
            game_character.show_all_chars(characters);

            Console.Write("\nВыберите персонажа: ");
            int choice = (int.Parse(Console.ReadLine()) - 1);
            int distance = 0;

            if (characters[choice].get_alive())
            {
                if (choice >= 0 && choice < characters.Count)
                {
                    Console.Clear();

                    while (true)
                    {
                        characters[choice].info(true);

                        Console.WriteLine("\n1. Лечить себя");
                        Console.WriteLine("2. Лечить союзников");

                        ConsoleKeyInfo healChoice = Console.ReadKey(true);

                        switch (healChoice.Key)
                        {
                            case ConsoleKey.D1:
                                characters[choice].heal_self();
                                Console.WriteLine();
                                break;

                            case ConsoleKey.D2:
                                Console.Clear();
                                Console.WriteLine("Выберите союзника для лечения:");
                                game_character.show_all_chars_by_side(characters[choice]);
                                Console.Write("Введите номер союзника: ");
                                int allyChoice = int.Parse(Console.ReadLine()) - 1;

                                if (allyChoice >= 0 && allyChoice < characters.Count && characters[allyChoice].get_alive() && characters[allyChoice].get_side() == characters[choice].get_side())
                                {
                                    characters[choice].heal_team(allyChoice);
                                }
                                else
                                {
                                    Console.WriteLine("Неверный выбор союзника.");
                                }

                                break;


                            default:
                                Console.WriteLine("Неверный выбор. Повторите попытку.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неверный выбор персонажа.\n");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Этот персонаж мертв.\n");
            }
        }
    }
}
