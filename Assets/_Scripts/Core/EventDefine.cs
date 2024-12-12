public partial class EventDefine: IEventParam {
    public struct OnPointsAdded: IEventParam {
        public int points;
    }

    public struct OnEnemyDead: IEventParam { 
        public EnemyBase enemy;
    }

    public struct OnLabelRecognized: IEventParam {
        public LabelManager.LabelType labelType;
    }

    public struct OnWinGame: IEventParam { }

    public struct OnLoseGame: IEventParam { }
}