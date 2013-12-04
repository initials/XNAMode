#import "Player.h"

@interface Liselot : Player //Enemy
{
//    BOOL dying;
//    BOOL readyToSwitchCharacters;
//    BOOL isMale;
    BOOL isTalking;

}

+ (id) liselotWithOrigin:(CGPoint)Origin;
- (id) initWithOrigin:(CGPoint)Origin;
- (void) talk:(BOOL)startTalking;

//@property BOOL dying;
//@property    BOOL readyToSwitchCharacters;
//@property     BOOL isMale;
@property     BOOL isTalking;


@end
