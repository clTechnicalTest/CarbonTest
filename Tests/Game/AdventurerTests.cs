namespace Tests
{
	using System;
	using Moq;
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
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", "0", "0", initialOrientation, movements });
			Adventurer adventurer = new Adventurer(null, adventurerEntry);

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
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", "0", "0", initialOrientation, movements });
			Adventurer adventurer = new Adventurer(null, adventurerEntry);

			adventurer.Move();
			Assert.That(adventurer.Orientation, Is.EqualTo(expectedIntermediateOrientation));

			adventurer.Move();
			Assert.That(adventurer.Orientation, Is.EqualTo(expectedFinalOrientation));
		}

		[Test]
		public void SimpleMapOneAdventurerValidateRightCommandOnAllOrientation_Move_RotateToRight()
		{
			string initialOrientation = "N";
			string expectedSecondOrientation = "O";
			string expectedThirdOrientation = "S";
			string expectedFourthOrientation = "E";
			string expectedFinalOrientation = "N";
			string movements = "DDDD";
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", "0", "0", initialOrientation, movements });
			Adventurer adventurer = new Adventurer(null, adventurerEntry);

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
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", "0", "0", initialOrientation, movements });
			Adventurer adventurer = new Adventurer(null, adventurerEntry);

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
		public void SimpleMapOneAdventurerValidateMovingCommand_Move_VerifyThatMapMoveAdventurerIsCalledToWest()
		{
			string adventurerName = "adv";
			string initialOrientation = "O";
			string initialXAdventurer = "1";
			string initialYAdventurer = "1";
			string adventurerMovements = "A";
			int expectedXStep = 1;
			int expectedYStep = 0;
			Mock<IMap> mapMock = new Mock<IMap>();
			IMap map = mapMock.Object;
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { adventurerName, initialXAdventurer, initialYAdventurer, initialOrientation, adventurerMovements });
			Adventurer adventurer = new Adventurer(map, adventurerEntry);
			mapMock.Setup(m => m.MoveAdventurer(adventurerName, expectedXStep, expectedYStep));

			adventurer.Move();

			mapMock.Verify(m => m.MoveAdventurer(adventurerName, expectedXStep, expectedYStep), Times.Once);
		}

		[Test]
		public void SimpleMapOneAdventurerValidateMovingCommand_Move_VerifyThatMapMoveAdventurerIsCalledToSouth()
		{
			string adventurerName = "adv";
			string initialOrientation = "S";
			string initialXAdventurer = "1";
			string initialYAdventurer = "1";
			string adventurerMovements = "A";
			int expectedXStep = 0;
			int expectedYStep = 1;
			Mock<IMap> mapMock = new Mock<IMap>();
			IMap map = mapMock.Object;
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { adventurerName, initialXAdventurer, initialYAdventurer, initialOrientation, adventurerMovements });
			Adventurer adventurer = new Adventurer(map, adventurerEntry);
			mapMock.Setup(m => m.MoveAdventurer(adventurerName, expectedXStep, expectedYStep));

			adventurer.Move();

			mapMock.Verify(m => m.MoveAdventurer(adventurerName, expectedXStep, expectedYStep), Times.Once);
		}

		[Test]
		public void SimpleMapOneAdventurerValidateMovingCommand_Move_VerifyThatMapMoveAdventurerIsCalledToNorth()
		{
			string adventurerName = "adv";
			string initialOrientation = "N";
			string initialXAdventurer = "1";
			string initialYAdventurer = "1";
			string adventurerMovements = "A";
			int expectedXStep = 0;
			int expectedYStep = -1;
			Mock<IMap> mapMock = new Mock<IMap>();
			IMap map = mapMock.Object;
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { adventurerName, initialXAdventurer, initialYAdventurer, initialOrientation, adventurerMovements });
			Adventurer adventurer = new Adventurer(map, adventurerEntry);
			mapMock.Setup(m => m.MoveAdventurer(adventurerName, expectedXStep, expectedYStep));

			adventurer.Move();

			mapMock.Verify(m => m.MoveAdventurer(adventurerName, expectedXStep, expectedYStep), Times.Once);
		}

		[Test]
		public void SimpleMapOneAdventurerValidateMovingCommand_Move_VerifyThatMapMoveAdventurerIsCalledToEast()
		{
			string adventurerName = "adv";
			string initialOrientation = "E";
			string initialXAdventurer = "1";
			string initialYAdventurer = "1";
			string adventurerMovements = "A";
			int expectedXStep = -1;
			int expectedYStep = 0;
			Mock<IMap> mapMock = new Mock<IMap>();
			IMap map = mapMock.Object;
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { adventurerName, initialXAdventurer, initialYAdventurer, initialOrientation, adventurerMovements });
			Adventurer adventurer = new Adventurer(map, adventurerEntry);
			mapMock.Setup(m => m.MoveAdventurer(adventurerName, expectedXStep, expectedYStep));

			adventurer.Move();

			mapMock.Verify(m => m.MoveAdventurer(adventurerName, expectedXStep, expectedYStep), Times.Once);
		}

		[Test]
		public void AdventurerWithOneAction_HasAction_ReturnsFalseAfterOneMoveCall()
		{
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "name", "0", "0", "O", "D" });
			Adventurer adventurer = new Adventurer(null, adventurerEntry);

			bool initialHasAction = adventurer.HasAction;
			adventurer.Move();
			bool finalHasAction = adventurer.HasAction;

			Assert.That(initialHasAction, Is.True);
			Assert.That(finalHasAction, Is.False);
		}
	}
}
