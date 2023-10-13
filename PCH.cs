using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Characters
{
    class game_character
    {
        private static List<game_character> characters = new List<game_character>();
        private const int max_x = 15;
        private const int max_y = 15;
        private string name;
        private int x;
        private int y;
        private bool side;
        private const int max_hp = 100;
        private int hp;
        private bool alive;
        private const int damage = 20;
        private const int heal = 20;

        public void new_chars(string name, int x, int y, bool side, int hp, bool alive)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.side = side;
            this.hp = hp;
            this.alive = alive;
            characters.Add(this);
            
        }

        public void info(bool show = true)
        {
            if (show)
            {
                Console.WriteLine("------------------------------");
            }
            Console.WriteLine($"Имя: {name}, Координаты: ({x}, {y}), Лагерь: {(side ? "Добрый" : "Злой")}, Здоровье: {hp}, Cтатус: {(alive ? "Жив" : "Мертв")}");
            if (show)
            {
                Console.WriteLine("------------------------------");
            }
        }

        public void move_up(int dx)
        {
            if (x != max_x)
            {
                if (x + dx <= max_x)
                {
                    x += dx;
                    Console.WriteLine("Передвижение успешно");
                }
                else
                {
                    Console.WriteLine("Передвижение невозможно, выберите меньшую дистанцию");
                }
            }
            else
            {
                Console.WriteLine("Передвижение невозможно, вы находитесь на краю карты");
            }
          
        }

        public void move_down(int dx)
        {
                if (x != 1)
                {
                    if (x - dx >= 1)
                    {
                        x -= dx;
                        Console.WriteLine("Передвижение успешно");
                    }
                    else
                    {
                        Console.WriteLine("Передвижение невозможно, выберите меньшую дистанцию");
                    }
                }
                else
                {
                    Console.WriteLine("Передвижение невозможно, вы находитесь на краю карты");
                }
        }

        public void move_left(int dy)
        {
            
            
                if (y != max_y)
                {
                    if (y + dy <= max_y)
                    {
                        y += dy;
                        Console.WriteLine("Передвижение успешно");
                    }
                    else
                    {
                        Console.WriteLine("Передвижение невозможно, выберите меньшую дистанцию");
                    }
                }
                else
                {
                    Console.WriteLine("Передвижение невозможно, вы находитесь на краю карты");
                }
        }

        public void move_right(int dy)
        {

                if (y <= max_y )
                {
                    if (y - dy <= max_y)
                    {
                        y -= dy;
                        Console.WriteLine("Передвижение успешно");
                    }
                    else
                    {
                        Console.WriteLine("Передвижение невозможно, выберите меньшую дистанцию");
                    }
                }
                else
                {
                    Console.WriteLine("Передвижение невозможно, вы находитесь на краю карты");
                }

        }

        public void kill()
        {
            hp = 0;
            alive = false;
            Console.WriteLine($"Персонаж {name} убит");
        }

        public void atack(int to_character)
        {
            game_character target = characters[to_character];

            if (is_near(target)) // Проверка, находятся ли они рядом
            {
                if (characters[to_character].side != side)
                {
                    if (characters[to_character].alive)
                    {
                        if (characters[to_character].hp <= damage)
                        {
                            characters[to_character].hp = 0;
                            characters[to_character].alive = false;

                            Console.WriteLine($"Вы убили персонажа {characters[to_character].name}");
                        }
                        else
                        {
                            characters[to_character].hp -= damage;

                            Console.WriteLine(
                                $"Вы нанесли персонажу {characters[to_character].name} {damage} урона. Его текущее здоровье: {characters[to_character].hp}  ");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Невозможно атаковать, этот персонаж мертв");
                    }
                }
                else
                {
                    Console.WriteLine("Невозможно атаковать, персонажа вашей же фракции");
                }
            }
            else
            {
                Console.WriteLine("Невозможно атаковать, цель слишком далеко");
            }

        }

        public void heal_team(int to_character)
        {
            game_character target = characters[to_character];

            if (is_near(target)) // Проверка, находятся ли они рядом
            {
                if (characters[to_character].side != side)
                {
                    if (!characters[to_character].alive)
                    {
                        if (characters[to_character].hp + heal >= max_hp)
                        {
                            characters[to_character].hp = max_hp;

                            Console.WriteLine($"Персонаж {characters[to_character].name} полностью здоров");
                        }
                        else
                        {
                            characters[to_character].hp += heal;

                            Console.WriteLine(
                                $"Вы восстановили персонажу {characters[to_character].name} {heal} здоровья. Его текущее здоровье: {characters[to_character].hp} ");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Невозможно лечить, этот персонаж мертв");
                    }
                }
                else
                {
                    Console.WriteLine("Невозможно лечить персонажа из чужой фракции");
                }
            }
            else
            {
                Console.WriteLine("Невозможно атаковать, цель слишком далеко");
            }
        }

        public void heal_self()
        {
        
            if (hp + heal >= max_hp)
            {
                hp = max_hp;

                Console.WriteLine($"Ваш персонаж полностью здоров");
            }
            else
            {
                hp += heal;

                Console.WriteLine($"Ваш персонаж восстановил {heal} здоровья. Его текущее здоровье: {hp} ");
            }
                
        }

        public void full_heal()
        {
            hp = 100;
            Console.WriteLine($"Персонаж {name} вылечен");
        }

        public static void show_all_charsinfo()
        {
            Console.WriteLine("------------------------------");
            foreach (var character in characters)
            {
                character.info(show: false);
                Console.WriteLine("------------------------------");
            }
        }

        public string get_name()
        {
            return name;
        }

        public bool get_alive()
        {
            return alive;
        }

        public bool get_side()
        {
            return side;
        }

        public void delete_account1(int c)
        {
            characters.RemoveAt(c);
        }

        public void change_side()
        {
            side = !side;

            Console.WriteLine($"Теперь персонаж {(side ? "добрый" : "злой")}");
        }

        public void rebirth()
        {
            if (!alive)
            {
                alive = true;
                full_heal(); // Восстановление здоровья до максимума
                Console.WriteLine($"Персонаж {name} воскрешен");
            }
            else
            {
                Console.WriteLine($"Персонаж {name} уже жив");
            }
        }

        public static void show_all_chars_by_side(game_character curreant_player)
        {
            Console.WriteLine("------------------------------");
            foreach (var character in characters)
            {
                if (curreant_player.is_near(character) && curreant_player.get_side() == character.get_side())
                {
                    character.info(show: false);
                    Console.WriteLine("------------------------------");
                }
            }
        }

        public static void show_all_dead()
        {
            Console.WriteLine("------------------------------");
            for (int i = 0; i < characters.Count; i++)
            {
                if (!characters[i].alive)
                {
                    Console.WriteLine($"{i + 1}. {characters[i].name}");
                    Console.WriteLine("------------------------------");
                }
            }
        }

        public bool is_near(game_character other)
        {
            return Math.Abs(x - other.x) <= 1 && Math.Abs(y - other.y) <= 1;
        }

        public static void show_all_chars(List<game_character> characters)
        {
            Console.WriteLine("------------------------------");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].get_name()}");
                Console.WriteLine("------------------------------");
            }
            
        }
    }
}
