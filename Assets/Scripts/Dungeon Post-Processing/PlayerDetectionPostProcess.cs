using Edgar.Unity;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Edgar/PostProcess/Custom/PlayerDetection", fileName = "PlayerDetectionPostProcess")]
public class PlayerDetectionPostProcess : DungeonGeneratorPostProcessBase
{
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        foreach (var roomInstance in level.GetRoomInstances())
        {
            var roomTemplateInstance = roomInstance.RoomTemplateInstance;

            var tilemaps = RoomTemplateUtils.GetTilemaps(roomTemplateInstance);
            var floor = tilemaps.Single(x => x.name == "Floor").gameObject;

            floor.layer = LayerMask.NameToLayer("Floor");
            AddFloorCollider(floor);
        }
    }

    protected void AddFloorCollider(GameObject floor)
    {
        var tilemapCollider2D = floor.AddComponent<TilemapCollider2D>();
        tilemapCollider2D.usedByComposite = true;

        var compositeCollider2d = floor.AddComponent<CompositeCollider2D>();
        compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
        compositeCollider2d.isTrigger = true;

        compositeCollider2d.generationType = CompositeCollider2D.GenerationType.Manual;
        floor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
