using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopingText : MonoBehaviour
{
    public static string[] RockText =
    {
        "멀리서 장애물이 보인다면 방향을 미리 조절해 놓으세요.",
        "장애물이 더 가까워지기 전에 방향을 확 틀어야 합니다.",
        "주변을 둘러보면서 충돌 사고에 항상 유의해주세요."
    };

    public static string[] WindWeatherText =
    {
        "바람이 부는 방향의 반대 방향으로 타륜을 돌려야 합니다.",
        "바람이 부는 방향을 잘 보고 빠르게 대처해보세요.",
        "바람이 불 때는 장애물과 안전거리를 유지하세요.",
        "멀더라도 기후의 영향이 없는 곳으로 안전하게 항해하세요."
    };

    public static string[] RainWeatherText =
    {
        "멀더라도 기후의 영향이 없는 곳으로 안전하게 항해하세요.",
        "비가 온다면 최대한 물살이 심하지 않은 곳으로 이동하세요."
    };

    public static string[] RainstormWeatherText =
    {
        "바람이 부는 방향의 반대 방향으로 타륜을 돌려야 합니다.",
        "바람이 심할 때는 타륜의 방향을 섬세하게 조절해야 합니다.",
        "바람이 심할 때는 장애물과 최대한 먼 거리를 유지하세요.",
        "기후의 영향이 없거나 덜한 곳으로 이동하세요."
    };
}
