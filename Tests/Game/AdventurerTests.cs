namespace Tests
{
	using System;
	using NUnit.Framework;
	using TreasureMap.Game;
	using TreasureMap.Maps;

	public class AdventurerTests
	{
		[Test]
		public void SimpleMapOneAdventurer_Move_ChangeTheOrientationToWest()
		{
			string initialOrientation = "N";
			string expectedOrientation = "O";
			string movements = "D";
			Map map = new Map(1, 1);
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", "0", "0", initialOrientation, movements });
			Adventurer adventurer = new Adventurer(map, adventurerEntry);

			adventurer.Move();

			Assert.That(adventurer.Orientation, Is.EqualTo(expectedOrientation));
		}

		[Test]
		public void SimpleMapOneAdventurerOneMoveRightOneMoveLeft_Move_DontChangeOrientation()
		{
			string initialOrientation = "S";
			string expectedIntermediateOrientation = "E";
			string expectedFinalOrientation = "S";
            string movements = "DG";
            Map map = new Map(1, 1);
            MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", "0", "0", initialOrientation, movements });
            Adventurer adventurer = new Adventurer(map, adventurerEntry);

            adventurer.Move();
            Assert.That(adventurer.Orientation, Is.EqualTo(expectedIntermediateOrientation));

			adventurer.Move();
			Assert.That(adventurer.Orientation, Is.EqualTo(expectedFinalOrientation));
		}

		[Test]
		public void SimpleMapOneAdventurerValidateRightCommandOnAllOrientation_Move_RotateToRight(){
			string initialOrientation = "N";
            string expectedSecondOrientation = "O";
			string expectedThirdOrientation = "S";
			string expectedFourthOrientation = "E";
			string expectedFinalOrientation = "N";
            string movements = "DDDD";
            Map map = new Map(1, 1);
            MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", "0", "0", initialOrientation, movements });
            Adventurer adventurer = new Adventurer(map, adventurerEntry);

            adventurer.Move();
            Assert.That(adventurer.Orientation, Is.EqualTo(expectedSecondOrientation));

			adventurer.Move();
            Assert.That(adventurer.Orientation, Is.EqualTo(expectedThirdOrientation));

			adventurer.Move();
            Assert.That(adventurer.Orientation, Is.EqualTo(expectedFourthOrientation));

			adventurer.Move();
            Assert.That(adventurer.Orientation, Is.EqualTo(expectedFinalOrientation));
		}

		[Test]
		public void SimpleMapOneAdventurerValidateLeftCommandOnAllOrientation_Move_RotateToLeft()
		{
			string initialOrientation = "N";
			string expectedSecondOrientation = "E";
			string expectedThirdOrientation = "S";
			string expectedFourthOrientation = "O";
			string expectedFinalOrientation = "N";
			string movements = "GGGG";
			Map map = new Map(1, 1);
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", "0", "0", initialOrientation, movements });
			Adventurer adventurer = new Adventurer(map, adventurerEntry);

			adventurer.Move();
			Assert.That(adventurer.Orientation, Is.EqualTo(expectedSecondOrientation));

			adventurer.Move();
			Assert.That(adventurer.Orientation, Is.EqualTo(expectedThirdOrientation));

			adventurer.Move();
			Assert.That(adventurer.Orientation, Is.EqualTo(expectedFourthOrientation));

			adventurer.Move();
			Assert.That(adventurer.Orientation, Is.EqualTo(expectedFinalOrientation));
		}
	}
}
