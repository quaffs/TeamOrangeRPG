using GameLibrary;
using GenericRPG.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace GenericRPG
{
    public partial class FrmArena : Form
    {
        private Game game;
        private Character character;
        private Enemy enemy;
        private Random rand;

        private double runchance;

        private SoundPlayer simpleAttackSound;
        private SoundPlayer magicAttackSound;
        private SoundPlayer healSound;
        private SoundPlayer runSound;

        public FrmArena()
        {
            InitializeComponent();

            // hides the [X] button so player can't skip a fight
            ControlBox = false;
        }
        private void btnEndFight_Click(object sender, EventArgs e)
        {
            EndFight();
        }
        private void EndFight()
        {
            Game.GetGame().ChangeState(GameState.ON_MAP);
            Close();
        }
        private void FrmArena_Load(object sender, EventArgs e)
        {
            rand = new Random();

            game = Game.GetGame();
            character = game.Character;
            enemy = new Enemy(rand.Next(character.Level + 1), Resources.enemy);

            // stats
            UpdateStats();

            // pictures
            picCharacter.BackgroundImage = character.Pic.BackgroundImage;
            picEnemy.BackgroundImage = enemy.Img;
            if (character.EquippedWeapon != null)
            {
                var picPlayerWeapon = new PictureBox();
                picPlayerWeapon.Size = new Size(64, 64);
                picPlayerWeapon.BackgroundImage = Resources.ResourceManager.GetObject(character.EquippedWeapon.Sprite) as Bitmap;
                picCharacter.Controls.Add(picPlayerWeapon);
                picPlayerWeapon.Location = new Point(180, 30);
                picPlayerWeapon.BackColor = Color.Transparent;
            }

            // names
            lblPlayerName.Text = character.Name;
            lblEnemyName.Text = enemy.Name;

            runchance = 0.25;

            simpleAttackSound = new SoundPlayer(@"Resources\attack.wav");
            magicAttackSound = new SoundPlayer(@"Resources\magic.wav");
            runSound = new SoundPlayer(@"Resources\run.wav");
        }

        public void UpdateStats()
        {
            lblPlayerLevel.Text = character.Level.ToString();
            lblPlayerHealth.Text = Math.Round(character.Health).ToString();
            lblPlayerStr.Text = Math.Round(character.GetModifiedStat("Str")).ToString();
            lblPlayerDef.Text = Math.Round(character.GetModifiedStat("Def")).ToString();
            lblPlayerMana.Text = Math.Round(character.Mana).ToString();
            lblPlayerXp.Text = Math.Round(character.XP).ToString();

            lblEnemyLevel.Text = enemy.Level.ToString();
            lblEnemyHealth.Text = Math.Round(enemy.Health).ToString();
            lblEnemyStr.Text = Math.Round(enemy.GetModifiedStat("Str")).ToString();
            lblEnemyDef.Text = Math.Round(enemy.GetModifiedStat("Def")).ToString();
            lblEnemyMana.Text = Math.Round(enemy.Mana).ToString();

            lblPlayerHealth.Text = Math.Round(character.Health).ToString();
            lblEnemyHealth.Text = Math.Round(enemy.Health).ToString();
        }

        private void btnSimpleAttack_Click(object sender, EventArgs e)
        {
            if (game.SoundOn)
            {
                simpleAttackSound.Play();
            }

            float prevEnemyHealth = enemy.Health;
            character.SimpleAttack(enemy);
            float enemyDamage = (float)Math.Round(prevEnemyHealth - enemy.Health);
            lblEnemyDamage.Text = enemyDamage.ToString();
            lblEnemyDamage.Visible = true;
            tmrEnemyDamage.Enabled = true;
            if (enemy.Health <= 0)
            {
                lblEnemyDamage.Visible = false;
                lblPlayerDamage.Visible = false;
                character.GainXP(enemy.XpDropped);
                lblEndFightMessage.Text = "You Gained " + Math.Round(enemy.XpDropped) + " xp!";
                lblEndFightMessage.Visible = true;
                Refresh();
                Thread.Sleep(1200);
                EndFight();
                if (character.ShouldLevelUp)
                {
                    FrmLevelUp frmLevelUp = new FrmLevelUp();
                    frmLevelUp.Show();
                }
            }
            else
            {
                doEnemyAttack();
            }
        }

        private void btnMagicAttack_Click(object sender, EventArgs e)
        {
            if (character.Mana < 10)
            {
                return;
            }

            if (game.SoundOn)
            {
                magicAttackSound.Play();
            }

            float prevEnemyHealth = enemy.Health;
            character.MagicAttack(enemy, character);
            float enemyDamage = (float)Math.Round(prevEnemyHealth - enemy.Health);
            lblEnemyDamage.Text = enemyDamage.ToString();
            lblEnemyDamage.Visible = true;
            tmrEnemyDamage.Enabled = true;
            if (enemy.Health <= 0)
            {
                lblEnemyDamage.Visible = false;
                lblPlayerDamage.Visible = false;
                character.GainXP(enemy.XpDropped);
                lblEndFightMessage.Text = "You Gained " + Math.Round(enemy.XpDropped) + " xp!";
                lblEndFightMessage.Visible = true;
                Refresh();
                Thread.Sleep(1200);
                EndFight();
                if (character.ShouldLevelUp)
                {
                    FrmLevelUp frmLevelUp = new FrmLevelUp();
                    frmLevelUp.Show();
                }
            }
            else
            {
                doEnemyAttack();
            }
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (character.Health <= 0)
            {
                UpdateStats();
                game.ChangeState(GameState.DEAD);
                lblEndFightMessage.Text = "You Were Defeated!";
                lblEndFightMessage.Visible = true;
                Refresh();
                Thread.Sleep(1200);
                EndFight();
                FrmGameOver frmGameOver = new FrmGameOver();
                frmGameOver.Show();
            }
            if (rand.NextDouble() < runchance)
            {
                if (game.SoundOn)
                {
                    runSound.Play();
                }

                lblEndFightMessage.Text = "You Ran Like a Coward!";
                lblEndFightMessage.Visible = true;
                Refresh();
                Thread.Sleep(1200);
                EndFight();
            }
            else
            {
                doEnemyAttack();
                runchance += 0.1;
            }
        }

        private void btnHeal_Click(object sender, EventArgs e)
        {
            if (character.Mana < 5 || character.Health >= (character.GetModifiedStat("MaxHealth") - 5))
            {
                return;
            }
            float prevPlayerHealth = character.Health;
            character.Heal(character);
            float playerDamage = (float)Math.Round(character.Health - prevPlayerHealth);
            lblPlayerDamage.ForeColor = Color.Green;
            lblPlayerDamage.Text = playerDamage.ToString();
            lblPlayerDamage.Visible = true;
            tmrPlayerDamage.Enabled = true;
            UpdateStats();
        }

        private void tmrPlayerDamage_Tick(object sender, EventArgs e)
        {
            lblPlayerDamage.Top -= 2;
            if (lblPlayerDamage.Top < 10)
            {
                lblPlayerDamage.Visible = false;
                tmrPlayerDamage.Enabled = false;
                lblPlayerDamage.Top = 52;
                lblPlayerDamage.ForeColor = Color.Red;
            }
        }

        private void tmrEnemyDamage_Tick(object sender, EventArgs e)
        {
            lblEnemyDamage.Top -= 2;
            if (lblEnemyDamage.Top < 10)
            {
                lblEnemyDamage.Visible = false;
                tmrEnemyDamage.Enabled = false;
                lblEnemyDamage.Top = 52;
            }
        }

        private void doEnemyAttack()
        {
            float prevPlayerHealth = character.Health;
            enemy.SimpleAttack(character);
            float playerDamage = (float)Math.Round(prevPlayerHealth - character.Health);
            lblPlayerDamage.Text = playerDamage.ToString();
            lblPlayerDamage.Visible = true;
            tmrPlayerDamage.Enabled = true;
            if (character.Health <= 0)
            {
                lblPlayerDamage.Visible = false;
                lblEnemyDamage.Visible = false;

                // check if player ran out of hearts:
                if (character.ShouldRespawn() == false)
                {
                    UpdateStats();
                    game.ChangeState(GameState.DEAD);
                    lblEndFightMessage.Text = "You Were Defeated!";
                    lblEndFightMessage.Visible = true;
                    Refresh();
                    Thread.Sleep(1200);
                    EndFight();
                    FrmGameOver frmGameOver = new FrmGameOver();
                    frmGameOver.Show();
                }
                else
                {
                    lblEndFightMessage.Text = "Hearts left: " + character.hearts + ". Respawning!";
                    lblEndFightMessage.Visible = true;
                    Refresh();
                    Thread.Sleep(1200);
                    EndFight();

                    //this.Close();     // BYR: I am trying to close old window
                    //Respawn();        //      but I can't figure it out how

                    character.RefillHealthAndMana();
                    character.BackToStart();

                }
            }
            else
            {
                UpdateStats();
            }
        }

        private void Respawn()
        {
            this.Close();
            Game.GetGame().ChangeState(GameState.ON_MAP);
            FrmMap frmMap = new FrmMap("Resources/titleScreen.txt");
            frmMap.ShowDialog();    // shows title screen
        }
    }
}