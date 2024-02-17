// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;
interface IERC20 {
    /**
     * @dev Returns the amount of tokens in existence.
     */
    function totalSupply() external view returns (uint256);

    /**
     * @dev Returns the amount of tokens owned by `account`.
     */
    function balanceOf(address account) external view returns (uint256);

    /**
     * @dev Moves `amount` tokens from the caller's account to `recipient`.
     *
     * Returns a boolean value indicating whether the operation succeeded.
     *
     * Emits a {Transfer} event.
     */
    function transfer(address recipient, uint256 amount) external returns (bool);

    /**
     * @dev Returns the remaining number of tokens that `spender` will be
     * allowed to spend on behalf of `owner` through {transferFrom}. This is
     * zero by default.
     *
     * This value changes when {approve} or {transferFrom} are called.
     */
    function allowance(address owner, address spender) external view returns (uint256);

    /**
     * @dev Sets `amount` as the allowance of `spender` over the caller's tokens.
     *
     * Returns a boolean value indicating whether the operation succeeded.
     *
     * IMPORTANT: Beware that changing an allowance with this method brings the risk
     * that someone may use both the old and the new allowance by unfortunate
     * transaction ordering. One possible solution to mitigate this race
     * condition is to first reduce the spender's allowance to 0 and set the
     * desired value afterwards:
     * https://github.com/ethereum/EIPs/issues/20#issuecomment-263524729
     *
     * Emits an {Approval} event.
     */
    function approve(address spender, uint256 amount) external returns (bool);

    /**
     * @dev Moves `amount` tokens from `sender` to `recipient` using the
     * allowance mechanism. `amount` is then deducted from the caller's
     * allowance.
     *
     * Returns a boolean value indicating whether the operation succeeded.
     *
     * Emits a {Transfer} event.
     */
    function transferFrom(address sender, address recipient, uint256 amount) external returns (bool);


    function decimals() external view returns (uint8);
    function name() external view returns (string memory);
    function symbol() external view returns (string memory);

    /**
     * @dev Emitted when `value` tokens are moved from one account (`from`) to
     * another (`to`).
     *
     * Note that `value` may be zero.
     */
    event Transfer(address indexed from, address indexed to, uint256 value);

    /**
     * @dev Emitted when the allowance of a `spender` for an `owner` is set by
     * a call to {approve}. `value` is the new allowance.
     */
    event Approval(address indexed owner, address indexed spender, uint256 value);
}
library SafeMath {
    /**
     * @dev Returns the addition of two unsigned integers, with an overflow flag.
     *
     * _Available since v3.4._
     */
    function tryAdd(uint256 a, uint256 b) internal pure returns (bool, uint256) {
        uint256 c = a + b;
        if (c < a) return (false, 0);
        return (true, c);
    }

    /**
     * @dev Returns the substraction of two unsigned integers, with an overflow flag.
     *
     * _Available since v3.4._
     */
    function trySub(uint256 a, uint256 b) internal pure returns (bool, uint256) {
        if (b > a) return (false, 0);
        return (true, a - b);
    }

    /**
     * @dev Returns the multiplication of two unsigned integers, with an overflow flag.
     *
     * _Available since v3.4._
     */
    function tryMul(uint256 a, uint256 b) internal pure returns (bool, uint256) {
        // Gas optimization: this is cheaper than requiring 'a' not being zero, but the
        // benefit is lost if 'b' is also tested.
        // See: https://github.com/OpenZeppelin/openzeppelin-contracts/pull/522
        if (a == 0) return (true, 0);
        uint256 c = a * b;
        if (c / a != b) return (false, 0);
        return (true, c);
    }

    /**
     * @dev Returns the division of two unsigned integers, with a division by zero flag.
     *
     * _Available since v3.4._
     */
    function tryDiv(uint256 a, uint256 b) internal pure returns (bool, uint256) {
        if (b == 0) return (false, 0);
        return (true, a / b);
    }

    /**
     * @dev Returns the remainder of dividing two unsigned integers, with a division by zero flag.
     *
     * _Available since v3.4._
     */
    function tryMod(uint256 a, uint256 b) internal pure returns (bool, uint256) {
        if (b == 0) return (false, 0);
        return (true, a % b);
    }

    /**
     * @dev Returns the addition of two unsigned integers, reverting on
     * overflow.
     *
     * Counterpart to Solidity's `+` operator.
     *
     * Requirements:
     *
     * - Addition cannot overflow.
     */
    function add(uint256 a, uint256 b) internal pure returns (uint256) {
        uint256 c = a + b;
        require(c >= a, "SafeMath: addition overflow");
        return c;
    }

    /**
     * @dev Returns the subtraction of two unsigned integers, reverting on
     * overflow (when the result is negative).
     *
     * Counterpart to Solidity's `-` operator.
     *
     * Requirements:
     *
     * - Subtraction cannot overflow.
     */
    function sub(uint256 a, uint256 b) internal pure returns (uint256) {
        require(b <= a, "SafeMath: subtraction overflow");
        return a - b;
    }

    /**
     * @dev Returns the multiplication of two unsigned integers, reverting on
     * overflow.
     *
     * Counterpart to Solidity's `*` operator.
     *
     * Requirements:
     *
     * - Multiplication cannot overflow.
     */
    function mul(uint256 a, uint256 b) internal pure returns (uint256) {
        if (a == 0) return 0;
        uint256 c = a * b;
        require(c / a == b, "SafeMath: multiplication overflow");
        return c;
    }

    /**
     * @dev Returns the integer division of two unsigned integers, reverting on
     * division by zero. The result is rounded towards zero.
     *
     * Counterpart to Solidity's `/` operator. Note: this function uses a
     * `revert` opcode (which leaves remaining gas untouched) while Solidity
     * uses an invalid opcode to revert (consuming all remaining gas).
     *
     * Requirements:
     *
     * - The divisor cannot be zero.
     */
    function div(uint256 a, uint256 b) internal pure returns (uint256) {
        require(b > 0, "SafeMath: division by zero");
        return a / b;
    }

    /**
     * @dev Returns the remainder of dividing two unsigned integers. (unsigned integer modulo),
     * reverting when dividing by zero.
     *
     * Counterpart to Solidity's `%` operator. This function uses a `revert`
     * opcode (which leaves remaining gas untouched) while Solidity uses an
     * invalid opcode to revert (consuming all remaining gas).
     *
     * Requirements:
     *
     * - The divisor cannot be zero.
     */
    function mod(uint256 a, uint256 b) internal pure returns (uint256) {
        require(b > 0, "SafeMath: modulo by zero");
        return a % b;
    }

    /**
     * @dev Returns the subtraction of two unsigned integers, reverting with custom message on
     * overflow (when the result is negative).
     *
     * CAUTION: This function is deprecated because it requires allocating memory for the error
     * message unnecessarily. For custom revert reasons use {trySub}.
     *
     * Counterpart to Solidity's `-` operator.
     *
     * Requirements:
     *
     * - Subtraction cannot overflow.
     */
    function sub(uint256 a, uint256 b, string memory errorMessage) internal pure returns (uint256) {
        require(b <= a, errorMessage);
        return a - b;
    }

    /**
     * @dev Returns the integer division of two unsigned integers, reverting with custom message on
     * division by zero. The result is rounded towards zero.
     *
     * CAUTION: This function is deprecated because it requires allocating memory for the error
     * message unnecessarily. For custom revert reasons use {tryDiv}.
     *
     * Counterpart to Solidity's `/` operator. Note: this function uses a
     * `revert` opcode (which leaves remaining gas untouched) while Solidity
     * uses an invalid opcode to revert (consuming all remaining gas).
     *
     * Requirements:
     *
     * - The divisor cannot be zero.
     */
    function div(uint256 a, uint256 b, string memory errorMessage) internal pure returns (uint256) {
        require(b > 0, errorMessage);
        return a / b;
    }

    /**
     * @dev Returns the remainder of dividing two unsigned integers. (unsigned integer modulo),
     * reverting with custom message when dividing by zero.
     *
     * CAUTION: This function is deprecated because it requires allocating memory for the error
     * message unnecessarily. For custom revert reasons use {tryMod}.
     *
     * Counterpart to Solidity's `%` operator. This function uses a `revert`
     * opcode (which leaves remaining gas untouched) while Solidity uses an
     * invalid opcode to revert (consuming all remaining gas).
     *
     * Requirements:
     *
     * - The divisor cannot be zero.
     */
    function mod(uint256 a, uint256 b, string memory errorMessage) internal pure returns (uint256) {
        require(b > 0, errorMessage);
        return a % b;
    }
}

import "@openzeppelin/contracts-upgradeable/token/ERC20/IERC20Upgradeable.sol";
import "@openzeppelin/contracts-upgradeable/access/OwnableUpgradeable.sol";
import "@openzeppelin/contracts-upgradeable/proxy/utils/UUPSUpgradeable.sol";

contract Launchbase is Initializable, UUPSUpgradeable, OwnableUpgradeable {

    using SafeMath for uint256;
    uint256 public totalPool = 0;

    struct PoolInfo {
        uint256 id;
        address token; // token to be transferred
        address adminWallet;
        uint256 startTime;
        uint256 endTime;
        uint256 totalRaised;
        uint256 softCap;
        uint256 hardCap;
        PoolState poolState;
        uint256 minContribution;
        uint256 maxContribution;
        string[3] _otherInfo;
        PoolCurrency[] supportedCurrencies;
    }
    struct PoolCurrency{
        address token;
        bool isActive;
        uint256 rate;
    }
    struct ContributorInfo {
        uint256 poolId;
        address contributor;
        uint256 amount;
        uint256 timestamp;
        address currencyUsed;
    }

    enum PoolState {
        InUse,
        Completed,
        Cancelled,
        NotStarted
    }

    mapping(uint256 => PoolInfo) public pools;
    ContributorInfo[] public allContributors;
    mapping(uint256 => uint256[]) public contributorsByPoolId;

    event PoolInitialized(uint256 indexed transactionId, address indexed admin, uint256 startTime, uint256 endTime);
    event Contribution(uint256 indexed transactionId, address indexed contributor, uint256 amount);

    function initialize(address _owner) initializer public {
        __Ownable_init(_owner);
    }

    function _authorizeUpgrade(address) internal override onlyOwner {}

    function sendViaCall(address payable _to) internal {
        uint256 senderBalance = _to.balance;
        require(senderBalance >= msg.value, "Insufficient balance");
        (bool sent, ) = _to.call{value: msg.value}("");
        require(sent, "Failed to send Ether");
    }

    function createPool(
        address _token,
        address _adminWallet,
        uint256 _startTime,
        uint256 _endTime,
        uint256 _softCap,
        uint256 _hardCap,
        uint256 _minContribution,
        uint256 _maxContribution,
        string[3] memory _otherInfo,
        address[] memory tokens,
        uint256[] memory rates,
        bool[] memory isTokens
    ) external {
        require(tokens.length == rates.length, "Array lengths do not match");
        uint256 poolId = generateTheNextPoolId();
        PoolInfo storage pool = pools[poolId];
        pool.id = poolId;
        pool.token = _token;
        pool.adminWallet = _adminWallet;
        pool.startTime = _startTime;
        pool.endTime = _endTime;
        pool.softCap = _softCap;
        pool.hardCap = _hardCap;
        pool.minContribution = _minContribution;
        pool.maxContribution = _maxContribution;
        pool._otherInfo = _otherInfo;
        pool.poolState = PoolState.InUse;

        // Assign supported currencies
        for (uint256 i = 0; i < tokens.length; i++) {
            pool.supportedCurrencies.push(PoolCurrency(tokens[i],isTokens[i], rates[i]));
        }
        totalPool+=1;
        emit PoolInitialized(poolId, _adminWallet, _startTime, _endTime);
    }

    function getAllPools() external view returns (PoolInfo[] memory) {
        PoolInfo[] memory allPools = new PoolInfo[](totalPool);
        for (uint256 i = 1; i <= totalPool; i++) {
            PoolInfo storage pool = pools[i];
            PoolInfo memory poolData;
            poolData.token = pool.token;
            poolData.adminWallet = pool.adminWallet;
            poolData.startTime = pool.startTime;
            poolData.endTime = pool.endTime;
            poolData.totalRaised = pool.totalRaised;
            poolData.softCap = pool.softCap;
            poolData.hardCap = pool.hardCap;
            poolData.poolState = pool.poolState;
            poolData.minContribution = pool.minContribution;
            poolData.maxContribution = pool.maxContribution;
            poolData._otherInfo = pool._otherInfo;
            poolData.supportedCurrencies = new PoolCurrency[](pool.supportedCurrencies.length);
            poolData.id = pool.id;
            for (uint256 j = 0; j < pool.supportedCurrencies.length; j++) {
                poolData.supportedCurrencies[j].token = pool.supportedCurrencies[j].token;
                poolData.supportedCurrencies[j].rate = pool.supportedCurrencies[j].rate;
                poolData.supportedCurrencies[j].isActive = pool.supportedCurrencies[j].isActive;
            }
            allPools[i-1] = poolData;
        }
        return allPools;
    }

    function contribute(address tokenAddress, uint256 tokenAmount, bool isToken, uint256 poolId) external payable {
        IERC20 token = IERC20(tokenAddress);
        uint256 currencyRate = getSupportedCurrencyRate(poolId, tokenAddress);
        PoolInfo storage pool = pools[poolId];
        require(pool.endTime >= block.timestamp, "Pool is closed");
        if (isToken) {
            // Transfer the accepted token amount to the owner
            uint256 senderBalance = token.balanceOf(msg.sender);
            string memory symbol =  token.symbol();
            require(senderBalance >= tokenAmount, string(abi.encodePacked("Insufficient ", symbol, " balance")));
            token.transferFrom(msg.sender, pool.adminWallet, tokenAmount);
        } else {
            require(msg.value > 0, "Insufficient ETH sent");
            sendViaCall(payable(pool.adminWallet)); // Send ETH to the owner
        }
        // Transfer the poolToken to the contributer
        IERC20 poolToken = IERC20(pool.token);
        uint256 convertedCurrency = 0;
        if(isToken==false)
        {
            convertedCurrency = convertCurrencyToToken(tokenAmount, currencyRate, getDecimal(pool.token));
        }
        else 
        {
            convertedCurrency = convertCurrencyToToken(tokenAmount, getDecimal(tokenAddress),getDecimal(pool.token),currencyRate);
        }
        poolToken.transferFrom(pool.adminWallet, msg.sender, convertedCurrency);
        pool.totalRaised = pool.totalRaised.add(tokenAmount);
        pool.poolState = checkPoolState(pool);
        ContributorInfo memory newContributor = ContributorInfo(poolId,msg.sender,tokenAmount,block.timestamp,tokenAddress);
        allContributors.push(newContributor);
        uint256 index = allContributors.length - 1;
        contributorsByPoolId[poolId].push(index);
        emit Contribution(index, tokenAddress, tokenAmount);
    }

    function getSupportedCurrencyRate(uint256 poolId, address _token) internal  view returns (uint256) {
        PoolInfo storage pool = pools[poolId];
        for (uint i = 0; i < pool.supportedCurrencies.length; i++) {
            if (pool.supportedCurrencies[i].token == _token) {
                return (pool.supportedCurrencies[i].rate);
            }
        }
        return 0;
    }
    function getContributors(uint256 _poolId) external view returns (ContributorInfo[] memory) {
        uint256[] memory contributorIndices = contributorsByPoolId[_poolId];
        ContributorInfo[] memory poolContributors = new ContributorInfo[](contributorIndices.length);
        for (uint256 i = 0; i < contributorIndices.length; i++) {
            poolContributors[i] = allContributors[contributorIndices[i]];
        }
        return poolContributors;
    }
    function convertCurrencyToToken(
        uint256 amount, 
        uint8 amountDecimals,
         uint8 expectedTokenDecimals,
          uint256 rate
    ) internal pure returns (uint256) {
        require(amountDecimals <= expectedTokenDecimals, "Amount decimals cannot be greater than expected token decimals");
        
        if (amountDecimals < expectedTokenDecimals) {
            return amount * (10 ** (uint256(expectedTokenDecimals) - uint256(amountDecimals))) * rate;
        } else if (amountDecimals > expectedTokenDecimals) {
            return amount * rate / (10 ** (uint256(amountDecimals) - uint256(expectedTokenDecimals)));
        } else {
            return amount * rate;
        }
    }
    function convertCurrencyToToken(
        uint256 amount,
        uint256 rate,
        uint256 decimals
    ) internal pure returns (uint256) {
        require(decimals <= 18, "Decimals should be less than or equal to 18");
        uint256 divisor = 10**(18 - decimals);
        return amount.mul(rate).div(divisor);
    }

    function getDecimal(address token) public view returns(uint8){
        uint8 decimals = IERC20(token).decimals();
        return decimals;
    }

    function generateTheNextPoolId() internal  view returns (uint256) {
        return totalPool + 1;
    }
    function checkPoolState(PoolInfo memory _poolInfo) internal view returns (PoolState) {
        uint256 currentTime = block.timestamp;

        if (currentTime < _poolInfo.startTime) {
            return PoolState.NotStarted;
        } else if (currentTime >= _poolInfo.startTime && currentTime <= _poolInfo.endTime) {
            return PoolState.InUse;
        } else {
            if (_poolInfo.totalRaised >= _poolInfo.softCap) {
                return PoolState.Completed;
            } else {
                return PoolState.Cancelled;
            }
        }
    }
    function checkPoolState(uint256 _poolid) public  view returns (PoolState) {
        PoolInfo memory pool = pools[_poolid];
        uint256 currentTime = block.timestamp;

        if (currentTime < pool.startTime) {
            return PoolState.NotStarted;
        } else if (currentTime >= pool.startTime && currentTime <= pool.endTime) {
            return PoolState.InUse;
        } else {
            if (pool.totalRaised >= pool.softCap) {
                return PoolState.Completed;
            } else {
                return PoolState.Cancelled;
            }
        }
    }
}
