from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from typing import Dict

app = FastAPI()

#Robot Location 0 -> 쓰래기장 / 1 -> 입구 / 2 -> 학생식당
# 로봇 위치 데이터베이스
robot_positions = {
    "robot1": 0,
    "robot2": 0,
    "robot3": 0
}

# 요청 본문 스키마
class LocationUpdate(BaseModel):
    location: int

@app.get("/robots")
def get_all_robot_positions() -> Dict[str, int]:
    return robot_positions

@app.get("/robot/{robot_id}")
def get_robot_position(robot_id: str) -> Dict[str, int]:
    if robot_id not in robot_positions:
        raise HTTPException(status_code=404, detail="존재하지 않는 로봇입니다")
    return {robot_id: robot_positions[robot_id]}

@app.put("/robot/{robot_id}")
def update_robot_position(robot_id: str, data: LocationUpdate):
    if robot_id not in robot_positions:
        raise HTTPException(status_code=404, detail="존재하지 않는 로봇입니다")

    if data.location not in [0, 1, 2]:
        raise HTTPException(status_code=400, detail="위치는 0, 1, 2 중 하나여야 합니다")

    robot_positions[robot_id] = data.location
    return {robot_id: robot_positions[robot_id]}
