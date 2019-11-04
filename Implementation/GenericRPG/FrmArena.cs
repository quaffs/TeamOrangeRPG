using GameLibrary;
using GenericRPG.Properties;
using System;
using System.Drawing;
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
		}

	public void UpdateStats() {
	  lblPlayerLevel.Text = character.Level.ToString();
	  lblPlayerHealth.Text = Math.Round(character.Health).ToString();
	  lblPlayerStr.Text = Math.Round(character.Stats["Str"].CalcValue).ToString();
	  lblPlayerDef.Text = Math.Round(character.Stats["Def"].CalcValue).ToString();
	  lblPlayerMana.Text = Math.Round(character.Mana).ToString();
	  lblPlayerXp.Text = Math.Round(character.XP).ToString();

	  lblEnemyLevel.Text = enemy.Level.ToString();
	  lblEnemyHealth.Text = Math.Round(enemy.Health).ToString();
	  lblEnemyStr.Text = Math.Round(enemy.Stats["Str"].CalcValue).ToString();
	  lblEnemyDef.Text = Math.Round(enemy.Stats["Def"].CalcValue).ToString();
	  lblEnemyMana.Text = Math.Round(enemy.Mana).ToString();

			lblPlayerHealth.Text = Math.Round(character.Health).ToString();
			lblEnemyHealth.Text = Math.Round(enemy.Health).ToString();
		}

		private void btnSimpleAttack_Click(object sender, EventArgs e)
		{
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
            double runchance = 0.25;
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
			if (character.Mana < 5 || character.Health >= (character.Stats["MaxHealth"].CalcValue - 5)) {
				return;
			}
			float prevPlayerHealth = character.Health;
			character.Heal(character);
			float playerDamage = (float)Math.Round(character.Health - prevPlayerHealth);
			lblPlayerDamage.Text = playerDamage.ToString();
			lblPlayerDamage.Visible = true;
			tmrPlayerDamage.Enabled = true;
		}

		private void tmrPlayerDamage_Tick(object sender, EventArgs e)
		{
			lblPlayerDamage.Top -= 2;
			if (lblPlayerDamage.Top < 10)
			{
				lblPlayerDamage.Visible = false;
				tmrPlayerDamage.Enabled = false;
				lblPlayerDamage.Top = 52;
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
					character.RefillHealthAndMana();
					character.BackToStart();
				}
			}
			else
			{
				UpdateStats();
			}
		}
	}
}