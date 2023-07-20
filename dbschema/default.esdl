module default {
    type Contact {
        required FirstName: str;
        required LastName: str;
        required Email: str;
        required Title: str;
        required Description: str;
        required DateOfBirth: datetime;
        required MarriageStatus: bool;
    }
}