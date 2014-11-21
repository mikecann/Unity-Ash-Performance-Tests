using Assets.Scripts.Systems;
using Net.RichardLord.Ash.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class AsteroidsGame : AshGame
    {

		void Awake()
		{
            Engine.AddSystem(new MovementSystem(), 0);
		}
	}    
}
